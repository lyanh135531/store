using Domain.Core;
using Domain.Ums.Entities;

namespace Domain.Ums.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
}