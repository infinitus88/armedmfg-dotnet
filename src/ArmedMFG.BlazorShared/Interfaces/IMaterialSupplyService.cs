using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IMaterialSupplyService
{
    Task<MaterialSupply> Create(CreateMaterialSupplyRequest materialSupplyRequest);
    Task<MaterialSupply> Edit(MaterialSupply materialSupply);
    Task<string> Delete(int id);
    Task<MaterialSupply> GetById(int id);
    Task<List<MaterialSupply>> ListPaged(int pageSize, int? materialTypeId);
    Task<List<MaterialSupply>> List();
}
