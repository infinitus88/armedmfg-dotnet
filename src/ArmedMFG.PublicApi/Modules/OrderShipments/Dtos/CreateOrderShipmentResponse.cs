using System;
using ArmedMFG.PublicApi.Modules.OrderShipments.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class CreateOrderShipmentResponse : BaseResponse
{
    public CreateOrderShipmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateOrderShipmentResponse()
    {
    }

    public OrderShipmentDto OrderShipment { get; set; }
}
