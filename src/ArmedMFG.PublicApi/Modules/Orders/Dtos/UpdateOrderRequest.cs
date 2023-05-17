using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.Orders.Dtos;

public class UpdateOrderRequest : BaseRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public string RequiredDate { get; set; }
    [Required]
    public string OrderedDate { get; set; }
    [Required]
    public string FinishDate { get; set; }
    [Required]
    public byte Status { get; set; }
    public string Description { get; set; }
}
