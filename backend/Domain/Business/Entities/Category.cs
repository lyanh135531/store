using Domain.Core;

namespace Domain.Business.Entities;

public class Category : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid? ParentId { get; set; }
    public Category Parent { get; set; }

    public List<Product> Products { get; set; }

    public List<Category> Children { get; set; }
}