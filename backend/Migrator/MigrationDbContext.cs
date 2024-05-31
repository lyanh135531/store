using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Migrator;

public class MigrationDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UmsEntities();
    }
}