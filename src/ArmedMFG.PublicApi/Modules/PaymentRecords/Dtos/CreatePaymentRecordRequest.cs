using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class CreatePaymentRecordRequest : BaseRequest
{
    [Required]
    public string? PayedDate { get; set; }

    [Required]
    public int PaymentCategoryId { get; set; }

    [Required]
    public int ReferenceId { get; set; }

    [Required]
    public byte PaymentMethod { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public string? Description { get; set; }
}
