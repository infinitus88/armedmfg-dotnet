using System;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

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
