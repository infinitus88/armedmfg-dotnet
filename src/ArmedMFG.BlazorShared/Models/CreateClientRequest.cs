﻿using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateClientRequest
{
    [Required(ErrorMessage = "The full name field is required")]
    public string FullName { get; set; }
    
    public string PhoneNumber { get; set; }
    public int OrganizationId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FindOutThrough { get; set; } = string.Empty;
}
