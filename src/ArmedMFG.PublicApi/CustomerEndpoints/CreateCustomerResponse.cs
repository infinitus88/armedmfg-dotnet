using System;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class CreateCustomerResponse : BaseResponse
{
    public CreateCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateCustomerResponse()
    {
    }

    public CustomerDto Customer { get; set; }
}
