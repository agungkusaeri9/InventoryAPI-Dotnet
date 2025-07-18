namespace InventoryApi_Dotnet.src.Application.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
    }
}
