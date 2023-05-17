using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class SearchOrderShipmentResponse : BaseResponse
{
    public SearchOrderShipmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchOrderShipmentResponse()
    {
    }

    public List<OrderShipmentDto> OrderShipments { get; set; } = new List<OrderShipmentDto>();
    public int TotalCount { get; set; }
}
