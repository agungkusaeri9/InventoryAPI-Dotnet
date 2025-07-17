using InventoryApi_Dotnet.src.Application.DTOs.Auth;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            var response = await _authRepository.LoginAsync(username, password);
            Console.WriteLine(response);
        }
    }
}
