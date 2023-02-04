using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin.Helpers;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorAdmin.Pages.CustomersPages;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public ICustomerService CustomerService { get; set; }

    [Microsoft.AspNetCore.Components.Inject]
    public ICustomerOrganizationService OrganizationService { get; set; }

    private List<Customer> _customers = new List<Customer>();
    private List<CustomerOrganization> _organizations = new List<CustomerOrganization>();

    private Create CreateComponent { get; set; }
    private Delete DeleteComponent { get; set; }
    // private MaterialTypePage.Edit EditComponent { get; set; }
    // private MaterialTypePage.Details DetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _customers = await CustomerService.List();
            
            // TODO Get Categories for specific department
            _organizations = await OrganizationService.List();
            _organizations.Add(new CustomerOrganization() { Id = 0, Name = "Not Set"});

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

    private async Task ReloadCustomers()
    {
        _customers = await CustomerService.List();
        StateHasChanged();
    }
}
