using System;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class UpdateCustomerResponse : BaseResponse
{
    public UpdateCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateCustomerResponse()
    {
    }
    
    public CustomerDto Customer { get; set; }
}
