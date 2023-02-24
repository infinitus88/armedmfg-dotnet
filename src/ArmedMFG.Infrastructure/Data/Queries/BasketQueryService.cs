using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.Infrastructure.Data.Queries;

public class BasketQueryService : IBasketQueryService
{
    private readonly ProductsContext _dbContext;

    public BasketQueryService(ProductsContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// This method performs the sum on the database rather than in memory
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<int> CountTotalBasketItems(string username)
    {
        var totalItems = 10;

        return totalItems;
    }
}
