namespace DotNetBusinessWorkFlow.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdateAt { get; protected set; }

    protected void MarkCreated()
    {
        CreatedAt = DateTime.UtcNow;
    }

    protected void MarkUpdated()
    {
        UpdateAt = DateTime.UtcNow;
    }
}

