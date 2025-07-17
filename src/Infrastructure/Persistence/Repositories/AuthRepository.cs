using InventoryApi_Dotnet.src.Application.DTOs.Auth;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
            return user != null;
        }
         
    }
}
