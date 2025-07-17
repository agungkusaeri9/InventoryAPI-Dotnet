using InventoryApi_Dotnet.src.Application.DTOs.Auth;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task <LoginResponseDTO> LoginAsync(string username, string password);
    }
}
