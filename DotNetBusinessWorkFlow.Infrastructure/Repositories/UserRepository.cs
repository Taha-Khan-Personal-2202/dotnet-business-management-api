using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkFlow.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
             .FirstOrDefaultAsync(f => f.Email == email);

    }
}
