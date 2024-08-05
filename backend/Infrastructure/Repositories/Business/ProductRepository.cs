using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class ProductRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<Product, Guid>(applicationDbContext), IProductRepository;