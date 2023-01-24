using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.ProductTypePage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IProductTypeService ProductTypeService { get; set; }

    [Microsoft.AspNetCore.Components.Inject]
    public ICatalogLookupDataService<ProductCategory> ProductCategoryService { get; set; }

    private List<ProductType> productTypes = new List<ProductType>();
    private List<ProductCategory> productCategories = new List<ProductCategory>();

    private Edit EditComponent { get; set; }
    private Delete DeleteComponent { get; set; }
    private Details DetailsComponent { get; set; }
    private Create CreateComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            productTypes = await ProductTypeService.List();
            productCategories = await ProductCategoryService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClick(int id)
    {
        await DetailsComponent.Open(id);
    }

    private async Task CreateClick()
    {
        await CreateComponent.Open();
    }

    private async Task EditClick(int id)
    {
        await EditComponent.Open(id);
    }

    private async Task DeleteClick(int id)
    {
        await DeleteComponent.Open(id);
    }

    private async Task ReloadProductCategories()
    {
        productCategories = await ProductCategoryService.List();
        StateHasChanged();
    }
}
