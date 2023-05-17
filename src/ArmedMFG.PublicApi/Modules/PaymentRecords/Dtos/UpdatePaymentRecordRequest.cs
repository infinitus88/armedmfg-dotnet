using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class UpdatePaymentRecordRequest : BaseRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string PayedDate { get; set; } = String.Empty;

    [Required]
    public int ReferenceId { get; set; }

    [Required]
    public byte PaymentMethod { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string? Description { get; set; }
}
