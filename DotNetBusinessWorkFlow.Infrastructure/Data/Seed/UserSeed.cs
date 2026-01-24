using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkflow.Infrastructure.Data.Seed;

public static class UserSeed
{
    private static readonly Guid AdminId =
        Guid.Parse("11111111-1111-1111-1111-111111111111");

    private static readonly Guid ManagerId =
        Guid.Parse("22222222-2222-2222-2222-222222222222");

    public static void Seed(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher();
        
        modelBuilder.Entity<User>().HasData(
            new User(
                "admin@company.com",
                passwordHasher.Hash("Admin@123"),
                UserRole.Admin
            )
            {
                Id = AdminId
            },

            new User(
                "manager@company.com",
                passwordHasher.Hash("Manager@123"),
                UserRole.Manager
            )
            {
                Id = ManagerId
            }
        );
    }
}
