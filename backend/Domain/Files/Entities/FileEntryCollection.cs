using Domain.Core;

namespace Domain.Files.Entities;

public class FileEntryCollection : Entity<Guid>
{
    public List<FileEntry> FileEntries { get; set; }
}