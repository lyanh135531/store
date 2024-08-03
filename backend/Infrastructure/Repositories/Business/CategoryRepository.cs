using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class CategoryRepository : EfCoreRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}