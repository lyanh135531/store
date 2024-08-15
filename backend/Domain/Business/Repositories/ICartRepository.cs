using Domain.Business.Entities;
using Domain.Core;

namespace Domain.Business.Repositories;

public interface ICartRepository : IRepository<Cart, Guid>
{
}