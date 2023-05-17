using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos.Shared;

namespace ArmedMFG.PublicApi.Modules.PaymentRecords.Dtos;

public class GetPaymentCategoryOptionsResponse : BaseResponse
{
    public GetPaymentCategoryOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetPaymentCategoryOptionsResponse()
    {
    }

    public List<PaymentCategoryOptionDto> PaymentCategories { get; set; } = new List<PaymentCategoryOptionDto>();
}

public class GetPaymentRecordForEditResponse : BaseResponse
{
    public GetPaymentRecordForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetPaymentRecordForEditResponse()
    {
    }

    public PaymentRecordForEditDto? PaymentRecord { get; set; }
}
