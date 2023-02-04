namespace ArmedMFG.BlazorShared.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public int OrganizationId { get; set; }
    public string Organization { get; set; }
    public string Email { get; set; }
    public string FindOutThrough { get; set; }
}
