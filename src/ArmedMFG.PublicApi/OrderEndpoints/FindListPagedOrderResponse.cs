using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class FindListPagedOrderResponse : BaseResponse
{
    public FindListPagedOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedOrderResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    public int TotalCount { get; set; }
}
