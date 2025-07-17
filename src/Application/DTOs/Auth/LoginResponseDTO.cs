namespace InventoryApi_Dotnet.src.Application.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public AccessTokenDTO AccessToken { get; set; } 
        public LoginDTO User { get; set; }
    }
}
