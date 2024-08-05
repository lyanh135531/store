using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class OrderRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<Order, Guid>(applicationDbContext), IOrderRepository;