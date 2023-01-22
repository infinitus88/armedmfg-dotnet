using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IMaterialTypeService
{
    Task<MaterialType> Create(CreateMaterialTypeRequest materialType);
    Task<MaterialType> Edit(MaterialType materialType);
    Task<string> Delete(int id);
    Task<MaterialType> GetById(int id);
    Task<List<MaterialType>> ListPaged(int pageSize);
    Task<List<MaterialType>> List(); 
}
