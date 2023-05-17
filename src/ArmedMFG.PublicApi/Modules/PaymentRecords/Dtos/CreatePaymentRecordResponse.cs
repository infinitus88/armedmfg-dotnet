using System;
using System.Text.Json.Serialization;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class CreatePaymentRecordResponse : BaseResponse
{
    public CreatePaymentRecordResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreatePaymentRecordResponse()
    {
    }

    public PaymentRecordDto PaymentRecord { get; set; }
}
