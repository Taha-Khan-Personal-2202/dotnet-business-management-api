using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; set; }
    public Money Price { get; set; }
    public bool IsActive { get; set; }

    private Product() { }

    public Product(string name, Money price)
    {
        Name = name;
        Price = price;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, Money price)
    {
        Name = name;
        Price = price;
        MarkUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
