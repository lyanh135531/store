namespace Domain.Core;

public class AuditableEntity : Entity<Guid>
{
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}