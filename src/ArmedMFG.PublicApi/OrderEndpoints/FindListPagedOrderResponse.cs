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

    public List<OrderInfoDto> Orders { get; set; } = new List<OrderInfoDto>();
    public int TotalCount { get; set; }
}
