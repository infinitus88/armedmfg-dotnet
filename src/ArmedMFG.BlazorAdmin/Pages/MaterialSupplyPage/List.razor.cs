using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.MaterialSupplyPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IMaterialSupplyService MaterialSupplyService { get; set; }
    
    [Microsoft.AspNetCore.Components.Inject]
    public IMaterialTypeService MaterialTypeService { get; set; }

    private List<MaterialSupply> _materialSupplies = new List<MaterialSupply>();
    private List<MaterialType> _materialTypes = new List<MaterialType>();

    private Create CreateComponent { get; set; }
    private Delete DeleteComponent { get; set; }
    // private MaterialTypePage.Edit EditComponent { get; set; }
    // private MaterialTypePage.Details DetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _materialSupplies = await MaterialSupplyService.List();
            _materialTypes = await MaterialTypeService.List();

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

    private async Task ReloadMaterialSupplies()
    {
        _materialSupplies = await MaterialSupplyService.List();
        StateHasChanged();
    }
}
