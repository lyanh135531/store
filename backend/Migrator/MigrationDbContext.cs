using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Migrator;

public class MigrationDbContext : DbContext
{
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UmsEntities();
    }
}