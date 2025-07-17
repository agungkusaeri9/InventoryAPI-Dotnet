using InventoryApi_Dotnet.src.Application.DTOs.Auth;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class AuthService
    {

        public Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            return new LoginResponseDTO;
        }
    }
}
