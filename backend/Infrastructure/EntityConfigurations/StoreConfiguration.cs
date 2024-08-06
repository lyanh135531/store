using Domain.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public static class StoreConfiguration
{
    private const string Schema = "store";

    public static void StoreEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable(nameof(Product), Schema);
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Price).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);

            entity.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable(nameof(Category), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable(nameof(Order), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Address).HasMaxLength(200);

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable(nameof(OrderDetail), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            entity.HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}