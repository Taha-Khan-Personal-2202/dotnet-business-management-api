using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class User : AuditableEntity
{
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    private User() { } // EF Core

    public User(string email, string passwordHash, UserRole role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
