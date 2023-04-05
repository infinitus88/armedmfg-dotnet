using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class PaymentRecordFilterSpecification : Specification<PaymentRecord>
{
    public PaymentRecordFilterSpecification(DateTime? startDate, DateTime? endDate, int? paymentCategoryId)
    {
        Query.Where(pr => (!startDate.HasValue || pr.PayedDate >= startDate) &&
                          (!endDate.HasValue || pr.PayedDate <= endDate));
    }
}
