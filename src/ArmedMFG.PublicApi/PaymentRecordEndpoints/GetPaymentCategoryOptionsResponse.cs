using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.PaymentRecordEndpoints;

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
