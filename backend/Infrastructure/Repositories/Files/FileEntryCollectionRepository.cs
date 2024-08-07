using Domain.Files.Entities;
using Domain.Files.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Files;

public class FileEntryCollectionRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<FileEntryCollection, Guid>(applicationDbContext), IFileEntryCollectionRepository
{
}