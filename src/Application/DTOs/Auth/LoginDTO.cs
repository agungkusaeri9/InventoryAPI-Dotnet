using System.ComponentModel.DataAnnotations;

namespace InventoryApi_Dotnet.src.Application.DTOs.Auth
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username harus diisi.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password harus diisi.")]
        public string Password { get; set; } = string.Empty;

    }
}
