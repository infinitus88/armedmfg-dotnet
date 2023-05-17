using System;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class UpdatePaymentRecordResponse : BaseResponse
{
    public UpdatePaymentRecordResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdatePaymentRecordResponse()
    {
    }

    public PaymentRecordDto? PaymentRecord { get; set; }
}
