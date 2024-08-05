using Domain.Core;
using Domain.Ums.Entities;

namespace Domain.Business.Entities;

public class Order : AuditableEntity
{
    public DateTime Date { get; set; }
    public string Address { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }
}