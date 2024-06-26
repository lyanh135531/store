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
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Status).IsRequired().HasDefaultValue(true);
            entity.Property(x => x.FullName).HasMaxLength(100);
            entity.Property(x => x.DateOfBirth).HasDefaultValue(DateTime.Now);
            entity.Property(x => x.Gender)
                .HasConversion(v => v.ToString(), v => (Gender)Enum.Parse(typeof(Gender), v));
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable(nameof(Role), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Code).IsUnicode();
            entity.Property(x => x.Type)
                .HasConversion(v => v.ToString(), v => (RoleType)Enum.Parse(typeof(RoleType), v));
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable(nameof(UserRole), Schema);
            entity.HasKey(x => new { x.UserId, x.RoleId });

            entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable(nameof(RoleClaim), Schema);
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.Role)
                .WithMany(x => x.RoleClaims)
                .HasForeignKey(x => x.RoleId);
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.ToTable(nameof(UserClaim), Schema);
            entity.HasKey(x => x.Id);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable(nameof(UserToken), Schema);
            entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        });
    }
}