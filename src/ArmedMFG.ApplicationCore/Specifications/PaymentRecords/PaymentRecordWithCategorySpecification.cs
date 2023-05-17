using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.PaymentRecords;

public sealed class PaymentRecordWithCategorySpecification : Specification<PaymentRecord>, ISingleResultSpecification
{
    public PaymentRecordWithCategorySpecification(int paymentRecordId)
    {
        Query
            .Where(p => p.Id == paymentRecordId)
            .Include(p => p.PaymentCategory);
    }
}
