using Domain.Core;

namespace Domain.Business.Entities;

public class Product : AuditableEntity<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}