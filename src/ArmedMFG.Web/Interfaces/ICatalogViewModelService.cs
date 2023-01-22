using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArmedMFG.Web.Services;

public interface ICatalogViewModelService
{
    Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);
    Task<IEnumerable<SelectListItem>> GetBrands();
    Task<IEnumerable<SelectListItem>> GetTypes();
}
