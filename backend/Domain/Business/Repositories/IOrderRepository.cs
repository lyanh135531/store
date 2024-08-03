using Domain.Business.Entities;
using Domain.Core;

namespace Domain.Business.Repositories;

public interface IOrderRepository : IRepository<Order, Guid>
{
}