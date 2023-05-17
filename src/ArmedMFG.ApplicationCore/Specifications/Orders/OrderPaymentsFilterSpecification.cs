using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.PaymentRecordAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Orders;

public class OrderPaymentsFilterSpecification : Specification<PaymentRecord>
{
    public OrderPaymentsFilterSpecification(int refId)
    {
        Query.Where(p => p.PaymentCategoryId == 1 && p.ReferenceId == refId);
    }
}
