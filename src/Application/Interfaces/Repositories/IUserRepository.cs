using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<PaginatedList<User>> GetAllAsync(int pageNumber, int pageSize, string? keyword = null);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(User user);
    }
}