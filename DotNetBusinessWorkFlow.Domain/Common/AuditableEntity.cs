namespace DotNetBusinessWorkFlow.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; }

    public void MarkUpdated() => UpdateAt = DateTime.UtcNow;
}
