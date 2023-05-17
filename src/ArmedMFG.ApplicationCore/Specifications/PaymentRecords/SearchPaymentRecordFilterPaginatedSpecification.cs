using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.PaymentRecords;

public class SearchPaymentRecordFilterPaginatedSpecification : Specification<PaymentRecord>
{
    public SearchPaymentRecordFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, int? paymentCategoryId)
    {
        Query
            .Where(pr => (!startDate.HasValue || pr.PayedDate >= startDate) &&
                         (!endDate.HasValue || pr.PayedDate <= endDate))
            .Include(p => p.PaymentCategory)
            .Skip(skip).Take(take);
    }
}
