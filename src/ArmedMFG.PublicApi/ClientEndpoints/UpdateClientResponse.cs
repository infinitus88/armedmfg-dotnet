using System;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class UpdateClientResponse : BaseResponse
{
    public UpdateClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateClientResponse()
    {
    }
    
    public ClientDto Client { get; set; }
}
