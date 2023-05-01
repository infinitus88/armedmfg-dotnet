using System;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class PaymentRecordDto
{
    public int Id { get; set; }
    public string PayedDate { get; set; }
    public int ReferenceId { get; set; }
    public byte Type { get; set; }
    public int PaymentCategoryId { get; set; }
    public byte PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
