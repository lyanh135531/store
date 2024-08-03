using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UmsEntities();
        modelBuilder.StoreEntities();

        base.OnModelCreating(modelBuilder);
    }
}