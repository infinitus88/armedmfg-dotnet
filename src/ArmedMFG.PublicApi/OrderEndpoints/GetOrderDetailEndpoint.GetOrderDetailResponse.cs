using System;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class GetOrderDetailResponse : BaseResponse
{
    public GetOrderDetailResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetOrderDetailResponse()
    {
    }

    public OrderDetailDto OrderDetail { get; set; }
}
