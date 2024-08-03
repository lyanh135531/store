using Domain.Core;

namespace Domain.Business.Entities;

public class OrderDetail : Entity<Guid>
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}