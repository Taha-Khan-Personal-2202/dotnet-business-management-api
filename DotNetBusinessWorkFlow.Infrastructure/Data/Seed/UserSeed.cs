using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkflow.Infrastructure.Data.Seed;

public static class UserSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher();

        var adminId = Guid.NewGuid();
        var managerId = Guid.NewGuid();

        modelBuilder.Entity<User>().HasData(
            new User(
                "admin@company.com",
                passwordHasher.Hash("Admin@123"),
                UserRole.Admin
            )
            { Id = adminId },

            new User(
                "manager@company.com",
                passwordHasher.Hash("Manager@123"),
                UserRole.Manager
            )
            { Id = managerId }
        );
    }
}
