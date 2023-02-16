using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IOrderService
{
    Task<Order> Create(CreateOrderRequest orderRequest);
    Task<Order> Edit(Order order);
    Task<string> Delete(int id);
    Task<Order> GetById(int id);
    Task<List<Order>> ListPaged(int pageSize);
    Task<List<Order>> List();
}
