using System.Security.Cryptography;
using InventoryApi_Dotnet.src.Application.DTOs.Auth;
using InventoryApi_Dotnet.src.Application.DTOs.User;
using InventoryApi_Dotnet.src.Application.Interfaces;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IAuthRepository authRepository,
            IJwtService jwtService,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            var hasher = new PasswordHasherService();
            var user = await _authRepository.GetByUsernameAsync(username);
            if (user == null)
                throw new Exception("Username or password invalid.");

            var isValid = hasher.VerifyPassword(user.Password, password);
            if (!isValid)
                throw new Exception("Username or password invalid.");

            // Generate JWT access token
            var accessToken = _jwtService.GenerateToken(user);

            // Generate refresh token
            var refreshToken = GenerateRefreshToken();

            // Get metadata from request
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();

            var accessTokenDto = _jwtService.GenerateToken(user); 

            accessTokenDto.RefreshToken = refreshToken.Token;
            accessTokenDto.ExpiredAt = refreshToken.ExpiresAt;

            // Save refresh token to database
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresAt,
                IpAddress = ip,
                UserAgent = userAgent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _authRepository.AddRefreshTokenAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            // Return DTO
            return new LoginResponseDTO
            {
                User = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Username = user.Username,
                    Role = user.Role.ToString()
                },
                AccessToken = accessTokenDto
            };
        }

        private (string Token, DateTime ExpiresAt) GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            string token = Convert.ToBase64String(randomBytes);
            DateTime expiresAt = DateTime.UtcNow.AddDays(7);

            return (token, expiresAt);
        }

        public async Task LogoutAsync(RefreshTokensDTO dto)
        {
            var token = await _authRepository.GetRefreshTokenAsync(dto.RefreshToken);
            if (token == null)
                throw new Exception("Refresh token not found.");

            _authRepository.RemoveRefreshToken(token);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AccessTokenDTO?> RefreshAccessTokenAsync(RefreshTokensDTO dto)
        {
            var existingToken = await _authRepository.GetRefreshTokenAsync(dto.RefreshToken);
            if (existingToken == null)
                throw new Exception("Invalid refresh token.");

            if (existingToken.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Refresh token expired.");

            var user = await _authRepository.GetUserByIdAsync(existingToken.UserId);
            if (user == null)
                throw new Exception("User not found.");

            var accessTokenDto = _jwtService.GenerateToken(user);

            var newRefresh = GenerateRefreshToken();
            accessTokenDto.RefreshToken = newRefresh.Token;
            accessTokenDto.ExpiredAt = newRefresh.ExpiresAt;

            _authRepository.RemoveRefreshToken(existingToken);

            var refreshEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefresh.Token,
                ExpiresAt = newRefresh.ExpiresAt,
                IpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _authRepository.AddRefreshTokenAsync(refreshEntity);
            await _unitOfWork.SaveChangesAsync();

            return accessTokenDto;
        }


    }
}
