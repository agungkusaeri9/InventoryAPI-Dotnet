namespace InventoryApi_Dotnet.src.Application.DTOs.Auth
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}
