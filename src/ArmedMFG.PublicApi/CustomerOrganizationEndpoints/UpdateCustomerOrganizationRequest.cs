using System.ComponentModel.DataAnnotations;
using ArmedMFG.ApplicationCore.Entities;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class UpdateCustomerOrganizationRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The name field is required")]
    public string Name { get; set; }
    
    public string PhoneNumber { get;set; }
    
    [Required(ErrorMessage = "The main branch address is required")]
    public UpdateAddressRequest MainBranchAddress { get; set; }
    
    public string Email { get; set; }
    
    public string Description { get; set; }
}

public class UpdateAddressRequest
{
    public string Region { get; set; }
    public string District { get; set; }
    public string? Street { get; set; }
}
