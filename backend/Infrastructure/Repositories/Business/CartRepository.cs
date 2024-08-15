using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class CartRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<Cart, Guid>(applicationDbContext), ICartRepository;