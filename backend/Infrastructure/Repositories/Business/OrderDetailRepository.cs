using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Business;

public class OrderDetailRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<OrderDetail, Guid>(applicationDbContext), IOrderDetailRepository;