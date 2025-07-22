using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.User;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            try
            {
                var result = await _userService.GetAllAsync(pageIndex, pageSize, keyword);
                return ResponseFormatter.SuccessWithPagination(result, "Users retrieved successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _userService.GetByIdAsync(id);
                return ResponseFormatter.Success(result, "User retrieved successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO dto)
        {
            try
            {
                var result = await _userService.CreateAsync(dto);
                return ResponseFormatter.Success(result, "User created successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                var result = await _userService.UpdateAsync(id, dto);
                return ResponseFormatter.Success(result, "User updated successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return ResponseFormatter.Success(null, "User deleted successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }
    }
}