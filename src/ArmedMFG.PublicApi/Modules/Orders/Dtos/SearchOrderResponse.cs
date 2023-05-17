using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Orders.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class SearchOrderResponse : BaseResponse
{
    public SearchOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchOrderResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    public int TotalCount { get; set; }
}
