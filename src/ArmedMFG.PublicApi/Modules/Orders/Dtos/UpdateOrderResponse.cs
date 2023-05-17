using System;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class UpdateOrderResponse : BaseResponse
{
    public UpdateOrderResponse(Guid correlationId) : base(correlationId) { }
    public UpdateOrderResponse() { }

    public OrderDto Order { get; set; }
}
