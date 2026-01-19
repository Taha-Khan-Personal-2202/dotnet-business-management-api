using DotNetBusinessWorkFlow.Domain.Common;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class Customer : AuditableEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    private Customer() { }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
