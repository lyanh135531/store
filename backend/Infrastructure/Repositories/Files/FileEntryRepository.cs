using Domain.Files.Entities;
using Domain.Files.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Files;

public class FileEntryRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<FileEntry, Guid>(applicationDbContext), IFileEntryRepository
{
}