using InventoryApi_Dotnet.src.Application.DTOs.User;

namespace InventoryApi_Dotnet.src.Application.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public AccessTokenDTO AccessToken { get; set; } 
    }
}
