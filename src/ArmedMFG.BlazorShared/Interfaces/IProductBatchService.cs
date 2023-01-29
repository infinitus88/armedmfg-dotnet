using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IProductBatchService
{
    Task<ProductBatch> Create(CreateProductBatchRequest productBatchRequest);
    Task<ProductBatch> Edit(ProductBatch productBatch);
    Task<string> Delete(int id);
    Task<ProductBatch> GetById(int id);
    Task<List<ProductBatch>> ListPaged(int pageSize);
    Task<List<ProductBatch>> List(); 
}
