using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class User : AuditableEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    public User() { }
    public User(string email, string passwordHash, UserRole role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
