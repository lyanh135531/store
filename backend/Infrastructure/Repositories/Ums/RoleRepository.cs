using Domain.Ums.Entities;
using Domain.Ums.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Ums;

public class RoleRepository : EfCoreRepository<Role, Guid>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}