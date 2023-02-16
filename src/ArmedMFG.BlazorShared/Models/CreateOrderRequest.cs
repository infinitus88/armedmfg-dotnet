using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateOrderRequest
{
    [Required(ErrorMessage = "The OrderedDate field is required")]
    public DateTime OrderedDate { get; set; }
    
    [Required(ErrorMessage = "The RequiredDate field is required")]
    public DateTime RequiredDate { get; set; }
    
    public int CustomerId { get; set; }
    
    public byte Status { get; set; }
    
    public byte PaymentType { get; set; }
    
    public string Description { get; set; }
    
    public List<CreateOrderProductRequest> OrderProducts { get; set; }
    
}
