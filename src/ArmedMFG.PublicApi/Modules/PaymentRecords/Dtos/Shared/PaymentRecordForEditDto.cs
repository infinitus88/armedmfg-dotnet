using System;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;

public class PaymentRecordForEditDto
{
    public int Id { get; set; }
    public string PayedDate { get; set; } = String.Empty; 
    public int ReferenceId { get; set; }
    public byte PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
