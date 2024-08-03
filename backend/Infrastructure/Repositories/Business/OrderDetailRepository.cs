using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class OrderDetailRepository : EfCoreRepository<OrderDetail, Guid>, IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}