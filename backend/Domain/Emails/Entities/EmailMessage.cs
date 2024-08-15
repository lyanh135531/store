using Domain.Core;

namespace Domain.Emails.Entities;

public class EmailMessage : Entity<Guid>
{
    public EmailStatus Status { get; set; }
    public required string FromEmail { get; set; }
    public required string ToEmail { get; set; }
    public string? CcEmail { get; set; }
    public string? BccEmail { get; set; }
    public required string MjmlContent { get; set; }
    public string? Subject { get; set; }
    public required string HtmlContent { get; set; }
    public DateTime SentDate { get; set; }
}

public enum EmailStatus : sbyte
{
    Success,
    Error,
    Sending,
}