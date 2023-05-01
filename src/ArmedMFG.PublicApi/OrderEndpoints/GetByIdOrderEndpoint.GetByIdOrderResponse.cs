using System;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class GetByIdOrderResponse : BaseResponse
{
    public GetByIdOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdOrderResponse()
    {
    }

    public OrderDto Order { get; set; }
}
