using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedClientResponse
{
    public List<Client> Clients { get; set; } = new List<Client>();
    public int PageCount { get; set; } = 0;
}
