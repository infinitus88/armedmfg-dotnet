namespace ArmedMFG.BlazorShared.Models;

public class CustomerOrganization
{
    public string Name { get; set; }
    public string TIN {get; set; }
    public Address MainBranchAddress { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
}

public class Address
{
    public string Region { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
}
