using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class CreateOrderShipmentEndpoint : IEndpoint<IResult, CreateOrderShipmentRequest, IRepository<OrderShipment>, IRepository<Order>>
{
    private readonly IMapper _mapper;
    private DateParsingSettings _dateParsingSettings;

    public CreateOrderShipmentEndpoint(IMapper mapper, IOptions<DateParsingSettings> dateParsingSettings)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings.Value;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/order-shipments",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateOrderShipmentRequest request, IRepository<OrderShipment> orderShipmentRepository, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(request, orderShipmentRepository, orderRepository);
                })
            .Produces<CreateOrderResponse>()
            .WithTags("OrderShipmentEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateOrderShipmentRequest request,
        IRepository<OrderShipment> orderShipmentRepository, IRepository<Order> orderRepository)
    {
        var response = new CreateOrderShipmentResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        
        var existingOrder = await orderRepository.GetByIdAsync(request.OrderId);
        
        if (existingOrder == null)
        {
            throw new NotFoundException($"A order with Id: {request.OrderId} is not found");
        }
        
        var newOrderShipment = new OrderShipment(request.OrderId, 
            DateTime.ParseExact(request.ShipmentDate, _dateParsingSettings.DefaultInputDateFormat, CultureInfo.InvariantCulture),
            request.DriverName, request.DriverPhone, request.CarNumber, request.Destination);
        
        foreach (var shipmentProduct in request.ShipmentProducts)
        {
            newOrderShipment.AddShipmentProduct(shipmentProduct.ProductTypeId, shipmentProduct.Quantity);
        }
        
        newOrderShipment = await orderShipmentRepository.AddAsync(newOrderShipment);

        var dto = new OrderShipmentDto()
        {
            Id = newOrderShipment.Id,
            OrderId = newOrderShipment.OrderId,
            // CustomerFullName = newOrderShipment.Order?.Customer.FullName,
            ShipmentDate = newOrderShipment.ShipmentDate.ToString(_dateParsingSettings.DefaultDisplayDateFormat),
            DriverName = newOrderShipment.DriverName,
            DriverPhone = newOrderShipment.DriverPhone,
            CarNumber = newOrderShipment.CarNumber,
            Destination = newOrderShipment.Destination,
            ShipmentProducts = newOrderShipment.ShipmentProducts.Select(_mapper.Map<ShipmentProductDto>).ToList()
        };
        
        response.OrderShipment = dto;
        return Results.Created($"api/orders/shipments/{dto.Id}", response);
    }
}
