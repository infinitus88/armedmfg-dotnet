using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class ListAutocompleteCustomerResponse : BaseResponse
{
    public ListAutocompleteCustomerResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListAutocompleteCustomerResponse()
    {
    }

    public List<CustomerAutocompleteDto> Customers { get; set; } = new List<CustomerAutocompleteDto>();
}
