using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
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

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class CreateOrderEndpoint : IEndpoint<IResult, CreateOrderRequest, IRepository<Order>, IRepository<Customer>>
{
    private readonly IMapper _mapper;

    public CreateOrderEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/orders",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateOrderRequest request, IRepository<Order> orderRepository, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(request, orderRepository, customerRepository);
                })
            .Produces<CreateOrderResponse>()
            .WithTags("OrderEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateOrderRequest request,
        IRepository<Order> orderRepository, IRepository<Customer> customerRepository)
    {
        var response = new CreateOrderResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        
        var existingCustomer = await customerRepository.GetByIdAsync(request.CustomerId);
        
        if (existingCustomer == null)
        {
            throw new NotFoundException($"A order's customer with Id: {request.CustomerId} is not found");
        }
        
        var newOrder = new Order(request.CustomerId, request.OrderedDate, request.RequiredDate, request.TotalAmount, request.Description);

        newOrder.SetStatus(Status.Pending);
        
        foreach (var orderProduct in request.OrderProducts)
        {
            newOrder.AddOrderProduct(orderProduct.ProductTypeId, orderProduct.Quantity, orderProduct.Price);
        }
        
        newOrder = await orderRepository.AddAsync(newOrder);

        var dto = new OrderDto
        {
            Id = newOrder.Id,
            CustomerId = newOrder.CustomerId,
            OrderedDate = newOrder.OrderedDate,
            RequiredDate = newOrder.RequiredDate,
            FinishedDate = newOrder.FinishedDate,
            Status = (byte)newOrder.Status,
            OrderProducts = newOrder.OrderProducts.Select(_mapper.Map<OrderProductDto>).ToList(),
            OrderShipments = newOrder.OrderShipments.Select(_mapper.Map<OrderShipmentDto>).ToList(),
            Description = newOrder.Description,
        };
        
        response.Order = dto;
        return Results.Created($"api/orders/{dto.Id}", response);
    }
}
