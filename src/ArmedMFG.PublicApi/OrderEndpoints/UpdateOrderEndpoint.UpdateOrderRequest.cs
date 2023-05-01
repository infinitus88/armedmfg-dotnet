using System;
using System.ComponentModel.DataAnnotations;
using ArmedMFG.ApplicationCore.Entities;

namespace ArmedMFG.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The CustomerId field is required")]
    public int CustomerId { get; set; }
    
    public DateTime OrderedDate { get; set; }
    
    public DateTime RequiredDate { get; set; }
    
    public DateTime FinishedDate { get; set; }

    public byte Status { get; set; }
    
    public byte PaymentType { get; set; }
    
    public string Description { get; set; }
}
