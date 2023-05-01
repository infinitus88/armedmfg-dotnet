using System;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class DeleteOrderResponse : BaseResponse
{
    public DeleteOrderResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteOrderResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
