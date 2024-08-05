using Domain.Business.Entities;
using Domain.Core;

namespace Domain.Business.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<int> GetNextSequenceNumberAsync();
}