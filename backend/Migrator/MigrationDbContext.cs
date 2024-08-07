using Domain.Core;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Migrator;

public class MigrationDbContext : DbContext
{
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UmsEntities();
        modelBuilder.StoreEntities();
        modelBuilder.FileEntities();

        base.OnModelCreating(modelBuilder);
    }
}