using System;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class PaymentRecordDto
{
    public int Id { get; set; }
    public DateTime PayedDate { get; set; }
    public int ReferenceId { get; set; }
    public int PaymentCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
