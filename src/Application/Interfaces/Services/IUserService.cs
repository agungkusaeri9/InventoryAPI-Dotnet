using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.User;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<PaginatedList<UserDTO>> GetAllAsync(int pageIndex, int pageSize, string? keyword = null);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO?> GetByUsernameAsync(string username);
        Task<UserDTO?> GetByEmailAsync(string email);
        Task<UserDTO> CreateAsync(CreateUserDTO createUserDto);
        Task<UserDTO> UpdateAsync(int id, UpdateUserDTO updateUserDto);
        Task<bool> DeleteAsync(int id);
    }
}