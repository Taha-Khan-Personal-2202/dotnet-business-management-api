using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkFlow.Infrastructure.Repositories;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .ToListAsync();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(f => f.Email == email);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }
}
