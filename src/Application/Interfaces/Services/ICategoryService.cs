using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Category;
using InventoryAPI_Dotnet.src.Application.DTOs.Category;

namespace InventoryAPI_Dotnet.src.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto);
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto);
        Task<bool> DeleteAsync(int id);

    }
}