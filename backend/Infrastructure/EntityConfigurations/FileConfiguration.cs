using Domain.Core;
using Domain.Files;
using Domain.Files.Entities;
using Domain.Ums.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public static class FileConfiguration
{
    private const string Schema = "dbo";

    public static void FileEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileEntry>(entity =>
        {
            entity.ToTable(nameof(FileEntry), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Path).IsRequired().HasMaxLength(100);
            entity.Property(x => x.FullPath).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Extension).IsRequired().HasMaxLength(20);
            entity.Property(x => x.ContentType).IsRequired().HasMaxLength(100);

            entity.HasOne(x => x.FileEntryCollection)
                .WithMany(x => x.FileEntries)
                .HasForeignKey(x => x.FileEntryCollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable(nameof(Role), Schema);
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Code).HasMaxLength(100);
            entity.Property(x => x.Name).HasMaxLength(100);
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