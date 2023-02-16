using System;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class UpdateOrderResponse : BaseResponse
{
    public UpdateOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateOrderResponse()
    {
    }
    
    public OrderDto Order { get; set; }
}
