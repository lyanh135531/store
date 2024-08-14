using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Migrator;

public class MigrationDbContext(DbContextOptions<MigrationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UmsEntities();
        modelBuilder.StoreEntities();
        modelBuilder.FileEntities();
        modelBuilder.EmailEntities();

        base.OnModelCreating(modelBuilder);
    }
}