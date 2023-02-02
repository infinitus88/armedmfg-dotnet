using System;
using System.ComponentModel.DataAnnotations;
using ArmedMFG.ApplicationCore.Entities;

namespace ArmedMFG.PublicApi.ClientEndpoints;

public class UpdateClientRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    public int? OrganizationId { get; set; }
    
    [Required(ErrorMessage = "The full name field is required")]
    public string FullName { get; set; }
    
    public string PhoneNumber { get;set; }
    
    public string Email { get; set; }
    
    public string FindOutThrough { get; set; }
}
