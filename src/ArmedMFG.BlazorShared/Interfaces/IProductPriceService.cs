using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IProductPriceService
{
    Task<ProductPrice> Create(CreateProductPriceRequest productPriceRequest);
    Task<ProductPrice> Edit(ProductPrice productPrice);
    Task<string> Delete(int id);
    Task<ProductPrice> GetById(int id);
    Task<List<ProductPrice>> ListPaged(int pageSize, int? productTypeId);
    Task<List<ProductPrice>> List();
}
