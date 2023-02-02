using System;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class CreateClientResponse : BaseResponse
{
    public CreateClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateClientResponse()
    {
    }

    public ClientDto Client { get; set; }
}
