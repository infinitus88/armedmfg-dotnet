using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.ProducedProductsPages;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IProductBatchService ProductBatchService { get; set; }

    // [Microsoft.AspNetCore.Components.Inject]
    // public IProductsLookupDataService<MaterialCategory> MaterialCategoryService { get; set; }

    private List<ProductBatch> productBatches = new List<ProductBatch>();
    // private List<MaterialCategory> materialCategories = new List<MaterialCategory>();

    // private Edit EditComponent { get; set; }
    // private Delete DeleteComponent { get; set; }
    // private Details DetailsComponent { get; set; }
    // private Create CreateComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            productBatches = await ProductBatchService.List();
            
            // TODO Get Categories for specific department
            // materialCategories = await MaterialCategoryService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClick(int id)
    {
        // await DetailsComponent.Open(id);
    }

    private async Task CreateClick()
    {
        // await CreateComponent.Open();
    }

    private async Task EditClick(int id)
    {
        // await EditComponent.Open(id);
    }

    private async Task DeleteClick(int id)
    {
        // await DeleteComponent.Open(id);
    }

    private async Task ReloadProductBatchs()
    {
        productBatches = await ProductBatchService.List();
        StateHasChanged();
    }

    // private async Task ReloadMaterialCategories()
    // {
    //     materialCategories = await MaterialCategoryService.List();
    //     StateHasChanged();
    // }
}
