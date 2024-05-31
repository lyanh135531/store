using Domain.Core;
using Domain.Ums.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public static class UmsConfiguration
{
    private static readonly string Schema = "ums";

    public static void UmsEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(nameof(User), Schema);
            entity.HasKey();
            entity.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            entity.Property(x => x.Status).IsRequired().HasDefaultValue(true);
            entity.Property(x => x.FullName).HasMaxLength(100);
            entity.Property(x => x.DateOfBirth).HasDefaultValue(DateTime.Now);

            entity.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable(nameof(Role), Schema);
            entity.HasKey();
            entity.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            entity.Property(x => x.Code).IsUnicode();
            entity.Property(x => x.Type)
                .HasConversion(v => v.ToString(), v => (RoleType)Enum.Parse(typeof(RoleType), v));

            entity.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);
        });
    }
}