namespace InventoryApi_Dotnet.src.Application.DTOs.Auth
{
    public class AccessTokenDTO
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiredAt { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }

}
