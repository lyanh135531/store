using Domain.Core;

namespace Domain.Files.Entities;

public class FileEntry : AuditableEntity
{
    public required string FileName { get; set; }
    public required string Extension { get; set; }
    public int Length { get; set; }
    public required string ContentType { get; set; }

    public Guid? FileEntryCollectionId { get; set; }
    public FileEntryCollection FileEntryCollection { get; set; }
}