using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class GetCustomerOptionsResponse : BaseResponse
{
    public GetCustomerOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetCustomerOptionsResponse()
    {
    }

    public List<CustomerOptionDto> Customers { get; set; } = new List<CustomerOptionDto>();
}
