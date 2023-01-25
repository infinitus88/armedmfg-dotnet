using Ardalis.Specification.EntityFrameworkCore;
using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ProductsContext dbContext) : base(dbContext)
    {
    }
}
