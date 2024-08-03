using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class ProductRepository : EfCoreRepository<Product, Guid>, IProductRepository
{
    public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}