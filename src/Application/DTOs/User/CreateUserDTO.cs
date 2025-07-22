

using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.DTOs.User
{
    public class CreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Staff;
    }
}