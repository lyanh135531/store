using Domain.Core;

namespace Domain.Files;

public class FileEntryCollection : Entity<Guid>
{
    public List<FileEntry> FileEntries { get; set; }
}