using Domain.Core;

namespace Domain.Business.Entities;

public class CartDetail : AuditableEntity
{
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } 

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}