using System;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class GetCustomerForEditResponse : BaseResponse
{
    public GetCustomerForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetCustomerForEditResponse()
    {
    }

    public CustomerForEditDto? Customer { get; set; }
}
