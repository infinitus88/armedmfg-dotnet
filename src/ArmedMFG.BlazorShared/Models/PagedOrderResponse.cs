using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedOrderResponse
{
    public List<Order> Orders { get; set; } = new List<Order>();
    public int PageCount { get; set; } = 0;
}
