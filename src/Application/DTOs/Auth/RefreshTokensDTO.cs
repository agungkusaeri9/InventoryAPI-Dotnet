using System.ComponentModel.DataAnnotations;

namespace InventoryApi_Dotnet.src.Application.DTOs.Auth
{
    public class RefreshTokensDTO
    {
        [Required(ErrorMessage ="Refresh token harus diisi.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
