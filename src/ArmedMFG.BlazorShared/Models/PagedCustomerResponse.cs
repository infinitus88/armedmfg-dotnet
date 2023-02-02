using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedCustomerResponse
{
    public List<Customer> Customers { get; set; } = new List<Customer>();
    public int PageCount { get; set; } = 0;
}
