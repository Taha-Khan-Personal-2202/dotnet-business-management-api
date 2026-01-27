using DotNetBusinessWorkFlow.Domain.Common;
using System.Text.Json.Serialization;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class Customer : AuditableEntity
{
    [JsonPropertyOrder(2)]
    public string Name { get; private set; }

    [JsonPropertyOrder(3)]
    public string Email { get; private set; }

    [JsonPropertyOrder(4)]
    public bool IsActive { get; private set; }

    private Customer() { }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
        IsActive = true;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, string email, bool isActive)
    {
        Name = name;
        Email = email;
        IsActive = isActive;
        MarkUpdated();
    }


    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
