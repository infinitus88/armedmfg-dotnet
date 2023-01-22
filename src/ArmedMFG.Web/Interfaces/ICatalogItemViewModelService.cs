using System.Threading.Tasks;
using ArmedMFG.Web.ViewModels;

namespace ArmedMFG.Web.Interfaces;

public interface ICatalogItemViewModelService
{
    Task UpdateCatalogItem(CatalogItemViewModel viewModel);
}
