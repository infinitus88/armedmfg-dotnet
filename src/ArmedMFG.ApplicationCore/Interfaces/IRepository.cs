using Ardalis.Specification;

namespace ArmedMFG.ApplicationCore.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
