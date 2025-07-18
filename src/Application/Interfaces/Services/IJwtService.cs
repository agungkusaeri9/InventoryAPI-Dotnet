using InventoryApi_Dotnet.src.Application.DTOs.Auth;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IJwtService
    {

        AccessTokenDTO GenerateToken(User user);
    }
}
