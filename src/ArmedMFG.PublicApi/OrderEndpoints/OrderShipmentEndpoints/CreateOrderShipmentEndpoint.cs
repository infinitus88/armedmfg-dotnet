using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class CreateOrderShipmentEndpoint : IEndpoint<IResult, CreateOrderShipmentRequest, IRepository<OrderShipment>, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public CreateOrderShipmentEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/orders/shipments",
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
        
        var newOrderShipment = new OrderShipment(request.OrderId, request.DriverName, request.DriverPhone, request.CarNumber, request.ShipmentDate);
        
        foreach (var shipmentProduct in request.ShipmentProducts)
        {
            newOrderShipment.AddShipmentProduct(shipmentProduct.ProductTypeId, shipmentProduct.Quantity);
        }
        
        newOrderShipment = await orderShipmentRepository.AddAsync(newOrderShipment);

        var dto = new OrderShipmentDto()
        {
            Id = newOrderShipment.Id,
            OrderId = newOrderShipment.OrderId,
            DriverName = newOrderShipment.DriverName,
            DriverPhone = newOrderShipment.DriverPhone,
            CarNumber = newOrderShipment.CarNumber,
            ShipmentDate = newOrderShipment.ShipmentDate,
            ShipmentProducts = newOrderShipment.ShipmentProducts.Select(_mapper.Map<ShipmentProductDto>).ToList()
        };
        
        response.OrderShipment = dto;
        return Results.Created($"api/orders/shipments/{dto.Id}", response);
    }
}
