using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IProductTypeService
{
    Task<ProductType> Create(CreateProductTypeRequest productType);
    Task<ProductType> Edit(ProductType productType);
    Task<string> Delete(int id);
    Task<ProductType> GetById(int id);
    Task<List<ProductType>> ListPaged(int pageSize);
    Task<List<ProductType>> List(); 
}
