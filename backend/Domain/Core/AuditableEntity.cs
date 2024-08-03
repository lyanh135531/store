namespace Domain.Core;

public class AuditableEntity<TKey> : Entity<TKey>
{
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}