using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class PaymentRecordFilterPaginatedSpecification : Specification<PaymentRecord>
{
    public PaymentRecordFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, int? paymentCategoryId)
    {
        Query
            .Where(pr => (!startDate.HasValue || pr.PayedDate >= startDate) &&
                         (!endDate.HasValue || pr.PayedDate <= endDate))
            .Skip(skip).Take(take);
    }
}
