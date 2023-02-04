using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateCustomerOrganizationRequest
{
    [Required(ErrorMessage = "The name field is required")]
    public string Name { get; set; }
    public string TaxpayerIdNum { get; set; }
    public Address MainBranchAddress { get; set; } = new Address();
    public string PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
