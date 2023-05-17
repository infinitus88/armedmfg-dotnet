using System;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class UpdateOrderShipmentResponse : BaseResponse
{
    public UpdateOrderShipmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateOrderShipmentResponse()
    {
    }

    public OrderShipmentDto OrderShipment { get; set; }
}
