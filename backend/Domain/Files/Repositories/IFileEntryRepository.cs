using Domain.Core;
using Domain.Files.Entities;

namespace Domain.Files.Repositories;

public interface IFileEntryRepository : IRepository<FileEntry, Guid>
{
}