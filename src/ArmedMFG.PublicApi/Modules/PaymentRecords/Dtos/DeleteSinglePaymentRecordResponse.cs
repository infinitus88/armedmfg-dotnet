using System;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class DeleteSinglePaymentRecordResponse : BaseResponse 
{
    public DeleteSinglePaymentRecordResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteSinglePaymentRecordResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
