using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence
{
    public class DbSeeder
    {

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordHasher = new PasswordHasher<User>();

            if(!context.Users.Any())
            {
                var user = new User
                {
                    Username = "admin",
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Role = User.UserRole.Admin,
                    IsActive = true
                };

                user.Password = passwordHasher.HashPassword(user, "password");

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
