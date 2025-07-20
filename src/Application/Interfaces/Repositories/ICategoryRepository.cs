using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Category;
using InventoryAPI_Dotnet.src.Application.DTOs.Category;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<PaginatedList<CategoryDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);
        Task<CategoryDTO?> CreateAsync(CreateCategoryDTO dto);
        Task<CategoryDTO?> GetByCodeAsync(string code);
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}