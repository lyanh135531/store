using Domain.Emails.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public static class EmailConfiguration
{
    private const string Schema = "dbo";

    public static void EmailEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.ToTable(nameof(EmailMessage), Schema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.FromEmail).IsRequired().HasMaxLength(100);
            entity.Property(x => x.ToEmail).IsRequired().HasMaxLength(100);
            entity.Property(x => x.CcEmail).HasMaxLength(100);
            entity.Property(x => x.BccEmail).HasMaxLength(100);
            entity.Property(x => x.MjmlContent).IsRequired();
            entity.Property(x => x.Subject).IsRequired().HasMaxLength(100);
            entity.Property(x => x.HtmlContent).IsRequired();
            entity.Property(x => x.SentDate).HasDefaultValueSql("GETDATE()");
            entity.Property(x => x.Status)
                .HasConversion(v => v.ToString(), v => (EmailStatus)Enum.Parse(typeof(EmailStatus), v));
        });
    }
}