using System.ComponentModel.DataAnnotations;

namespace InventoryApi_Dotnet.src.Domain.Entities
{
    public enum UserRole
    {
        Admin, Staff
    };
    public class User : BaseEntity
    {

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Staff;
        public bool IsActive { get; set; } = true;

    }
}
