using InventoryApi_Dotnet.src.Application.DTOs.Auth;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task <LoginResponseDTO?> LoginAsync(string username, string password);
        Task LogoutAsync(RefreshTokensDTO RefreshToken);

        Task<AccessTokenDTO?> RefreshAccessTokenAsync(RefreshTokensDTO RefreshToken);

    }
}
