using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher = new();
        public string HashPassword(string? password)
        {
            return _hasher.HashPassword(null!, password!);
        }

        public bool VerifyPassword(string hashedPassword, string providePassword)
        {
            var result = _hasher.VerifyHashedPassword(null!, hashedPassword, providePassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
