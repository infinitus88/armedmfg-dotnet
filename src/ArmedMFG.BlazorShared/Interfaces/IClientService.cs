using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IClientService
{
    Task<Client> Create(CreateClientRequest client);
    Task<Client> Edit(Client client);
    Task<string> Delete(int id);
    Task<Client> GetById(int id);
    Task<List<Client>> ListPaged(int pageSize);
    Task<List<Client>> List();
}
