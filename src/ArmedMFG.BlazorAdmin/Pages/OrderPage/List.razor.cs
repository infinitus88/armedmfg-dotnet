using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.OrderPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService { get; set; }

    // [Microsoft.AspNetCore.Components.Inject]
    // public IProductsLookupDataService<MaterialCategory> MaterialCategoryService { get; set; }
    
    [Microsoft.AspNetCore.Components.Inject]
    public IProductTypeService ProductTypeService { get; set; }

    private List<Order> _orders = new List<Order>();

    private List<ProductType> productTypes = new List<ProductType>();
    // private List<MaterialCategory> materialCategories = new List<MaterialCategory>();

    // private Edit EditComponent { get; set; }
    private Delete DeleteComponent { get; set; }
    // private Details DetailsComponent { get; set; }
    private Create CreateComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _orders = await OrderService.List();
            
            // TODO Get types for specific department
            productTypes = await ProductTypeService.List();
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
        await CreateComponent.Open();
    }

    private async Task EditClick(int id)
    {
        // await EditComponent.Open(id);
    }

    private async Task DeleteClick(int id)
    {
        await DeleteComponent.Open(id);
    }

    private async Task ReloadOrders()
    {
        _orders = await OrderService.List();
        
        
        StateHasChanged();
    }

    // private async Task ReloadMaterialCategories()
    // {
    //     materialCategories = await MaterialCategoryService.List();
    //     StateHasChanged();
    // }
}
