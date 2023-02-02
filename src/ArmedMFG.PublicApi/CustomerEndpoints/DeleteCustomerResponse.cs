using System;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class DeleteCustomerResponse : BaseResponse
{
    public DeleteCustomerResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteCustomerResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
