using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class Order : AuditableEntity
{
    public Guid CustomerId { get; private set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; private set; }  // make setter private

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money TotalAmount { get; private set; }

    private readonly List<OrderItem> _items = new();

    private Order() { }

    public Order(Guid customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Created;
        TotalAmount = Money.Zero("INR");
        MarkCreated();
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Cannot modify order after confirmation.");

        _items.Add(item);
        RecalculateTotal();
    }

    public bool CanConfirm(out string? reason)
    {
        reason = null;

        if (!_items.Any())
        {
            reason = "Order must have at least one item to be confirmed.";
            return false;
        }

        if (Status != OrderStatus.Created)
        {
            reason = $"Order is already in {Status} state and cannot be confirmed.";
            return false;
        }

        return true;
    }

    public void Confirm()
    {
        if (!CanConfirm(out var reason))
            throw new InvalidOperationException(reason ?? "Cannot confirm order.");

        Status = OrderStatus.Confirmed;
        MarkUpdated();
    }

    public bool CanBePaid(out string? reason)
    {
        reason = null;

        if (Status != OrderStatus.Confirmed)
        {
            reason = $"Only confirmed orders can be paid. Current status: {Status}";
            return false;
        }

        return true;
    }

    public void MarkAsPaid()
    {
        if (!CanBePaid(out var reason))
            throw new InvalidOperationException(reason ?? "Cannot mark order as paid.");

        Status = OrderStatus.Paid;
        MarkUpdated();
    }

    public bool CanBeCompleted(out string? reason)
    {
        reason = null;

        if (Status != OrderStatus.Paid)
        {
            reason = $"Only paid orders can be completed. Current status: {Status}";
            return false;
        }

        return true;
    }

    public void Complete()
    {
        if (!CanBeCompleted(out var reason))
            throw new InvalidOperationException(reason ?? "Cannot complete order.");

        Status = OrderStatus.Completed;
        MarkUpdated();
    }

    public bool CanBeCancelled(out string? reason)
    {
        reason = null;

        if (Status == OrderStatus.Paid || Status == OrderStatus.Completed)
        {
            reason = $"Paid or completed orders cannot be cancelled. Current status: {Status}";
            return false;
        }

        return true;
    }

    public void Cancel()
    {
        if (!CanBeCancelled(out var reason))
            throw new InvalidOperationException(reason ?? "Cannot cancel order.");

        Status = OrderStatus.Cancelled;
        MarkUpdated();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _items
            .Select(i => i.GetTotal())
            .Aggregate(Money.Zero("INR"), (acc, next) => acc + next);
    }
}