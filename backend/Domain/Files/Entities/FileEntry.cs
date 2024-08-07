using Domain.Core;

namespace Domain.Files.Entities;

public class FileEntry : AuditableEntity
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public int Size { get; set; }
    public string ContentType { get; set; }

    public Guid? FileEntryCollectionId { get; set; }
    public FileEntryCollection FileEntryCollection { get; set; }
}