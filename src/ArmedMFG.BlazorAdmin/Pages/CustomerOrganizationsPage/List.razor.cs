using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.CustomerOrganizationsPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public ICustomerOrganizationService OrganizationService { get; set; }

    private List<CustomerOrganization> _organizations = new List<CustomerOrganization>();

    private Create CreateComponent { get; set; }
    private Delete DeleteComponent { get; set; }
    // private MaterialTypePage.Edit EditComponent { get; set; }
    // private MaterialTypePage.Details DetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _organizations = await OrganizationService.List();
            
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

    private async Task ReloadOrganizations()
    {
        _organizations = await OrganizationService.List();
        StateHasChanged();
    }
}
