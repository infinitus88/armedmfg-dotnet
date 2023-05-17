using System;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class DeleteSingleOrderResponse : BaseResponse
{
    public DeleteSingleOrderResponse(Guid correlationId) : base(correlationId) { }
    public DeleteSingleOrderResponse() { }

    public string Status { get; set; } = "Deleted";
}
