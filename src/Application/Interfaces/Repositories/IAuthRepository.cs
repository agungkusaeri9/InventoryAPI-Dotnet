using InventoryApi_Dotnet.src.Application.DTOs.Auth;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
