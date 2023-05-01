using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class SearchOrdersResponse : BaseResponse
{
    public SearchOrdersResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchOrdersResponse()
    {
    }

    public List<OrderSearchDto> Orders { get; set; } = new List<OrderSearchDto>();
    public int TotalCount { get; set; }
}
