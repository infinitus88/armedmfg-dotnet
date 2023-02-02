using System;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class GetByIdCustomerResponse : BaseResponse
{
    public GetByIdCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdCustomerResponse()
    {
    }

    public CustomerDto Customer { get; set; }
}
