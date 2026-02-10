using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Repositories;
using DotNetBusinessWorkFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkFlow.Infrastructure.Repositories;

public class InvoiceRepository(AppDbContext context) : IInvoiceRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
    }

    public async Task<Invoice?> GetByIdAsync(Guid id)
    {
        return await _context.Invoices
                    .Include(s => s.Items)
                    .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Invoice?> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.Invoices.
                    Include(i => i.Items)
                    .FirstOrDefaultAsync(i => i.OrderId == orderId);
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
                    .Include(i => i.Items)
                    .ToListAsync();
    }
}
