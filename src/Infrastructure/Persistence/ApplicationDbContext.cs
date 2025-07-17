using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(

                new User
                {
                    Id = 1,
                    Username = "Admin",
                    Email = "Admin@gmail.com",
                    Password = "Password",
                    Role = User.UserRole.Admin,
                    IsActive = true
                }
            );
        }
    }

}
