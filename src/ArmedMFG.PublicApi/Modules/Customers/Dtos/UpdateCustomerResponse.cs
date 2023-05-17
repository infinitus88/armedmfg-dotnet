using System;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class UpdateCustomerResponse : BaseResponse
{
    public UpdateCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateCustomerResponse()
    {
    }

    public CustomerDto? Customer { get; set; }
}
