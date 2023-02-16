using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class AutocompleteCustomerResponse
{
    public List<AutocompleteCustomer> Customers { get; set; } = new List<AutocompleteCustomer>();
}
