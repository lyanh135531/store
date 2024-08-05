using Domain.Core;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UmsEntities();
        modelBuilder.StoreEntities();

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateAuditableEntity();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntity()
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.Entity is AuditableEntity)
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .ToList();

        foreach (var entityEntry in entries)
        {
            var auditableEntity = (AuditableEntity)entityEntry.Entity;
            if (entityEntry.State == EntityState.Added)
            {
                auditableEntity.CreatedTime = DateTime.Now;
            }
            else
            {
                auditableEntity.LastModifiedTime = DateTime.Now;
            }
        }
    }
}