using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkFlow.Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{

    private readonly AppDbContext _context = context;

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
