using Domain.Core;
using Infrastructure.Core;
using Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ICurrentUser currentUser,
    IMediator mediator)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UmsEntities();
        modelBuilder.StoreEntities();
        modelBuilder.FileEntities();
        modelBuilder.EmailEntities();
    }

    public override int SaveChanges()
    {
        UpdateAuditableEntity();
        DispatchDomainEvents().GetAwaiter().GetResult();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntity();
        DispatchDomainEvents().GetAwaiter().GetResult();
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

    private async Task DispatchDomainEvents()
    {
        var entities = ChangeTracker
            .Entries<Entity<Guid>>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}