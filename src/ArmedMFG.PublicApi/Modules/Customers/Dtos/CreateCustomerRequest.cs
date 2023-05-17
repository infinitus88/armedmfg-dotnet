using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos;

public class CreateCustomerRequest : BaseRequest
{
    [Required]
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public byte Position { get; set; }
    public byte FindOutThrough { get; set; }
    public bool IsBusiness { get; set; }
    public string OrganizationName { get; set; }
    public string TaxId { get; set; }
    public string Description { get; set; }
}
