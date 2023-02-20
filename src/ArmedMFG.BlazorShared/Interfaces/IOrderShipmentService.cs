using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IOrderShipmentService
{
    Task<OrderShipment> Create(CreateOrderShipmentRequest orderShipmentRequest);
    Task<OrderShipment> Edit(OrderShipment orderShipment);
    Task<string> Delete(int id);
    Task<OrderShipment> GetById(int id);
    Task<List<OrderShipment>> ListPaged(int pageSize);
    Task<List<OrderShipment>> List();
}
