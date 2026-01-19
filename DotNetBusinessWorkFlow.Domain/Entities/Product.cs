using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public bool IsActive { get; private set; }

    private Product() { }

    public Product(string name, Money price)
    {
        Name = name;
        Price = price;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
