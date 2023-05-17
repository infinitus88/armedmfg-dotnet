using System;

namespace ArmedMFG.PublicApi.Modules.OrderShipments.Dtos;

public class DeleteSingleOrderShipmentResponse : BaseResponse
{
    public DeleteSingleOrderShipmentResponse(Guid correlationId) : base(correlationId) { }
    public DeleteSingleOrderShipmentResponse() { }

    public string Status { get; set; } = "Deleted";
}
