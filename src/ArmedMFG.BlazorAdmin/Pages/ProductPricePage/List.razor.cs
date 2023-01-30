using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.ProductPricePage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IProductPriceService ProductPriceService { get; set; }

    [Microsoft.AspNetCore.Components.Inject]
    public IProductTypeService ProductTypeService { get; set; }

    private List<ProductPrice> productPrices = new List<ProductPrice>();
    private List<ProductType> productTypes = new List<ProductType>();

    private Create CreateComponent { get; set; }
    private Delete DeleteComponent { get; set; }
    // private MaterialTypePage.Edit EditComponent { get; set; }
    // private MaterialTypePage.Details DetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            productPrices = await ProductPriceService.List();
            
            // TODO Get Categories for specific department
            productTypes = await ProductTypeService.List();

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
        await CreateComponent.Open();
    }

    private async Task EditClick(int id)
    {
        // await EditComponent.Open(id);
    }

    private async Task DeleteClick(int id)
    {
        // await DeleteComponent.Open(id);
    }

    private async Task ReloadProductPrices()
    {
        productPrices = await ProductPriceService.List();
        StateHasChanged();
    }
}
