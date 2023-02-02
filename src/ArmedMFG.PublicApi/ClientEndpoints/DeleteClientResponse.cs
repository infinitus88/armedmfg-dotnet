using System;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class DeleteClientResponse : BaseResponse
{
    public DeleteClientResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteClientResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
