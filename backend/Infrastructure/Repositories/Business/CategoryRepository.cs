using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class CategoryRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<Category, Guid>(applicationDbContext), ICategoryRepository;