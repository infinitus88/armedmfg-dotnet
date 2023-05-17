using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;

public class CustomerForEditDto
{
    [Required]
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public byte Position { get; set; }
    public byte FindOutThrough { get; set; }
    public int IsBusiness { get; set; }
    public string OrganizationName { get; set; }
    public int TaxId { get; set; }
    public string Description { get; set; }
}
