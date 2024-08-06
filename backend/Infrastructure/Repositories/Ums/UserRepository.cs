using Domain.Ums.Entities;
using Domain.Ums.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Ums;

public class UserRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<User, Guid>(applicationDbContext), IUserRepository;