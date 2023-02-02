using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderCatalogAggregate;

namespace ArmedMFG.ApplicationCore.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(int basketId, Address shippingAddress);
}
