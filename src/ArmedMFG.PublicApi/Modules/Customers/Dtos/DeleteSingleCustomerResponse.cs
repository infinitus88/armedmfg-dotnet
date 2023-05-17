using System;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class DeleteSingleCustomerResponse : BaseResponse
{
    public DeleteSingleCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteSingleCustomerResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
