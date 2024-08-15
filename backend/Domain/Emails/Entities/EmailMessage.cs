using Domain.Core;

namespace Domain.Emails.Entities;

public class EmailMessage : Entity<Guid>
{
    public EmailStatus Status { get; set; }
    public string FromEmail { get; set; }
    public string ToEmail { get; set; }
    public string CcEmail { get; set; }
    public string BccEmail { get; set; }
    public string MjmlContent { get; set; }
    public string Subject { get; set; }
    public string HtmlContent { get; set; }
    public DateTime SentDate { get; set; }
}

public enum EmailStatus : sbyte
{
    Success,
    Error,
    Sending,
}