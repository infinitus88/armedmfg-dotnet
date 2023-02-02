using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedOrganizationResponse
{
    public List<Organization> Organizations { get; set; } = new List<Organization>();
}
