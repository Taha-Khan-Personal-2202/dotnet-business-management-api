using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkFlow.Infrastructure.Repositories;

public class PaymentRepository(AppDbContext context) : IPaymentRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public async Task<Payment?> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
    }
}
