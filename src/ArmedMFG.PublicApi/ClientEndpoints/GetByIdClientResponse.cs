using System;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class GetByIdClientResponse : BaseResponse
{
    public GetByIdClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdClientResponse()
    {
    }

    public ClientDto Client { get; set; }
}
