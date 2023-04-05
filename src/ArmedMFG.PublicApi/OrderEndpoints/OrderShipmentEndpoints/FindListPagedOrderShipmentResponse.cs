using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

public class FindListPagedOrderShipmentResponse : BaseResponse
{
    public FindListPagedOrderShipmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedOrderShipmentResponse()
    {
    }

    public List<OrderShipmentInfoDto> OrderShipments { get; set; } = new List<OrderShipmentInfoDto>();
    public int TotalCount { get; set; }
}
