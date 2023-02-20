using System;

namespace ArmedMFG.PublicApi.OrderEndpoints.OrderShipmentEndpoints;

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
