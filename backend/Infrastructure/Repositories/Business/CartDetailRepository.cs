using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class CartDetailRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<CartDetail, Guid>(applicationDbContext), ICartDetailRepository;