using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class ListPagedOrderResponse : BaseResponse
{
    public ListPagedOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedOrderResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    public int PageCount {get; set; }
}
