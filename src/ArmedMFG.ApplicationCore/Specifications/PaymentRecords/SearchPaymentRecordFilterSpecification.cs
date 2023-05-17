using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.PaymentRecords;

public class SearchPaymentRecordFilterSpecification : Specification<PaymentRecord>
{
    public SearchPaymentRecordFilterSpecification(DateTime? startDate, DateTime? endDate, int? paymentCategoryId)
    {
        Query.Where(pr => (!startDate.HasValue || pr.PayedDate >= startDate) &&
                          (!endDate.HasValue || pr.PayedDate <= endDate))
            .Include(p => p.PaymentCategory);
    }
}
