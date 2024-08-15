using Domain.Core;
using Domain.Ums.Entities;

namespace Domain.Business.Entities;

public class Cart : Entity<Guid>
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public List<CartDetail> CartDetails { get; set; } = [];
}