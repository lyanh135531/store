using Domain.Core;
using Infrastructure.Core;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UmsEntities();
        modelBuilder.StoreEntities();
        modelBuilder.FileEntities();
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
                auditableEntity.CreatedBy = currentUser.Id;
            }
            else
            {
                auditableEntity.LastModifiedTime = DateTime.Now;
                auditableEntity.LastModifiedBy = currentUser.Id;
            }
        }
    }
}