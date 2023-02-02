using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class ListPagedClientResponse : BaseResponse
{
    public ListPagedClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedClientResponse()
    {
    }

    public List<ClientDto> Clients { get; set; } = new List<ClientDto>();
    public int PageCount {get; set; }
}
