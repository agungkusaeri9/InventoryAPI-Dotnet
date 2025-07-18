using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.Auth;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using InventoryApi_Dotnet.src.Domain.Entities;
using InventoryApi_Dotnet.src.Infrastructure.Persistence;
using InventoryApi_Dotnet.src.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;
        public AuthController(IAuthService authService, ApplicationDbContext context)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var response = await _authService.LoginAsync(dto.Username, dto.Password);
                Console.WriteLine(response);
                return ResponseFormatter.Success(response, "Login berhasil");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser()
        {
            var hasher = new PasswordHasherService();
            var newUser = new User
            {
                Name = "Admin",
                Username = "admin",
                Email = "admin@gmail.com",
                Password = hasher.HashPassword("password"),
                IsActive = true
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokensDTO dto)
        {
            try
            {
                await _authService.LogoutAsync(dto);
                return ResponseFormatter.Success("User Logout successfully.");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokensDTO dto)
        {
            try
            {
                var tokenResult = await _authService.RefreshAccessTokenAsync(dto);

                if (tokenResult == null)
                    return ResponseFormatter.Error("Invalid or expired refresh token.", 401);

                return Ok(tokenResult);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error("Error refreshing token: " + ex.Message);
            }
        }

    }
}
