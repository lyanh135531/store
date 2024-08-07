using Domain.Core;
using Domain.Files.Entities;

namespace Domain.Files.Repositories;

public interface IFileEntryCollectionRepository : IRepository<FileEntryCollection, Guid>
{
}