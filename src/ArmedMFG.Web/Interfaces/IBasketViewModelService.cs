using ArmedMFG.ApplicationCore.Entities.BasketAggregate;
using ArmedMFG.Web.Pages.Basket;

namespace ArmedMFG.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<int> CountTotalBasketItems(string username);
    Task<BasketViewModel> Map(Basket basket);
}
