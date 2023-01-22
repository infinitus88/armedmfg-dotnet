using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(int basketId, Address shippingAddress);
}
