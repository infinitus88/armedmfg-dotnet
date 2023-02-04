using System;

namespace ArmedMFG.BlazorShared.Models;

public class CustomerOrganization
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TaxpayerIdNum {get; set; }
    public string MainBranchAddress { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
}

public class Address
{
    public string Region { get; set; }
    public string District { get; set; }
    public string Street { get; set; }

    public Address()
    {
        Street = String.Empty;
    }
}
