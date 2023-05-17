using System;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class CreateOrderResponse : BaseResponse
{
    public CreateOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateOrderResponse()
    {
    }

    public OrderDto Order { get; set; }
}
