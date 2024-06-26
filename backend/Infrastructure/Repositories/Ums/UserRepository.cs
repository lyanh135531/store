using Domain.Ums.Entities;
using Domain.Ums.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Ums;

public class UserRepository : EfCoreRepository<User, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}