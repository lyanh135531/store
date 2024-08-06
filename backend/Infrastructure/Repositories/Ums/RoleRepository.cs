using Domain.Ums.Entities;
using Domain.Ums.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Ums;

public class RoleRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<Role, Guid>(applicationDbContext), IRoleRepository;