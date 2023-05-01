using System;
using System.Text.Json.Serialization;
using ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

public class CreatePaymentRecordResponse : BaseResponse
{
    public CreatePaymentRecordResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreatePaymentRecordResponse()
    {
    }

    [JsonPropertyName("paymentRecord")]
    public PaymentRecordDto PaymentRecord { get; set; }
}
