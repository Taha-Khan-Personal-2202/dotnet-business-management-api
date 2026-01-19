using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class Order : AuditableEntity
{
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public Money TotalAmount { get; private set; }

    private readonly List<OrderItem> _items = new();

    private Order() { }

    public Order(Guid customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Created;
        TotalAmount = new Money(1);
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Cannot modify order after confirmation.");

        _items.Add(item);
        RecalculateTotal();
    }

    public void Confirm()
    {
        if (!_items.Any())
            throw new InvalidOperationException("Order must have at least one item.");

        Status = OrderStatus.Confirmed;
        MarkUpdated();
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed orders can be paid.");

        Status = OrderStatus.Paid;
        MarkUpdated();
    }

    public void Complete()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Only paid orders can be completed.");

        Status = OrderStatus.Completed;
        MarkUpdated();
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid || Status == OrderStatus.Completed)
            throw new InvalidOperationException("Paid or completed orders cannot be cancelled.");

        Status = OrderStatus.Cancelled;
        MarkUpdated();
    }

    private void RecalculateTotal()
    {
        Money total = _items.First().GetTotal();

        foreach (var item in _items.Skip(1))
            total += item.GetTotal();

        TotalAmount = total;
    }
}
