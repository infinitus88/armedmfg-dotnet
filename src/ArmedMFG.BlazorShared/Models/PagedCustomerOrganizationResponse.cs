using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedCustomerOrganizationResponse
{
    public List<CustomerOrganization> Organizations { get; set; } = new List<CustomerOrganization>();
}
