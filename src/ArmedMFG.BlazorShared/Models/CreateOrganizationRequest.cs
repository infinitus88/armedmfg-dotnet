using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateOrganizationRequest
{
    [Required(ErrorMessage = "The name field is required")]
    public string Name { get; set; }
    public Address MainBranchAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
