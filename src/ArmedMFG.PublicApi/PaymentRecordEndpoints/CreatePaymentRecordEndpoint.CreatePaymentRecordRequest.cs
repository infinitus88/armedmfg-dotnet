using System;
using System.Text.Json.Serialization;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class CreatePaymentRecordRequest : BaseRequest
{
    [JsonPropertyName("paymentRecord")]
    public CreatePaymentRecordData? Data { get; set; }
}

public class CreatePaymentRecordData
{
    public string? PayedDate { get; set; }
    public int PaymentCategoryId { get; set; }
    public int ReferenceId { get; set; }
    public byte PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
