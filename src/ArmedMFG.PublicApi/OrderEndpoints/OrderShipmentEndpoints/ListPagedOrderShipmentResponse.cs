using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class ListPagedOrderShipmentResponse : BaseResponse
{
    public ListPagedOrderShipmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedOrderShipmentResponse()
    {
    }

    public List<OrderShipmentDto> OrderShipments { get; set; } = new List<OrderShipmentDto>();
    public int PageCount {get; set; }
}
