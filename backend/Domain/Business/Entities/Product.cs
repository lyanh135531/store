using Domain.Core;
using Domain.Files;
using Domain.Files.Entities;

namespace Domain.Business.Entities;

public class Product : AuditableEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public Guid FileEntryCollectionId { get; set; }
    public FileEntryCollection FileEntryCollection { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}