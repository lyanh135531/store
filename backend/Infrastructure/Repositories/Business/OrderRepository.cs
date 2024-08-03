using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class OrderRepository : EfCoreRepository<Order, Guid>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}