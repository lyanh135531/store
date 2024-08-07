using Domain.Core;

namespace Domain.Files.Entities;

public class FileEntry : AuditableEntity
{
    public string FileName { get; set; }
    public string Extension { get; set; }
    public int Length { get; set; }
    public string ContentType { get; set; }

    public Guid? FileEntryCollectionId { get; set; }
    public FileEntryCollection FileEntryCollection { get; set; }
}