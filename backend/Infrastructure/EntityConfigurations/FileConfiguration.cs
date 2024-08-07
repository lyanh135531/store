using Domain.Files.Entities;
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
            entity.Property(x => x.FileName).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Extension).IsRequired().HasMaxLength(20);
            entity.Property(x => x.ContentType).IsRequired().HasMaxLength(100);

            entity.HasOne(x => x.FileEntryCollection)
                .WithMany(x => x.FileEntries)
                .HasForeignKey(x => x.FileEntryCollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FileEntryCollection>(entity =>
        {
            entity.ToTable(nameof(FileEntryCollection), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
    }
}