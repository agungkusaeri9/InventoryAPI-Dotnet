using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Category;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryAPI_Dotnet.src.Application.DTOs.Category;
using InventoryAPI_Dotnet.src.Application.Interfaces.Services;

namespace InventoryAPI_Dotnet.src.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<PaginatedList<CategoryDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            var result = await _categoryRepository.GetAllAsync(pageIndex, pageSize, keyword);
            return result;
        }

        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var checkData = await _categoryRepository.GetByCodeAsync(dto.Code);
            if (checkData != null)
            {
                throw new Exception("Category with this code already exists.");
            }

            var result = await _categoryRepository.CreateAsync(dto);
            if (result == null)
            {
                throw new Exception("Failed to create category.");
            }
            return result;
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }
            return result;
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Category not found.");
            var result = await _categoryRepository.UpdateAsync(id, dto);
            if (result == null)
            {
                throw new Exception("Failed to update category.");
            }
            return result;
        } 

        public async Task<bool> DeleteAsync(int id)
        {
            var existing =  await _categoryRepository.GetByIdAsync(id);
            if(existing == null) throw new KeyNotFoundException("Category not found.");
            return await _categoryRepository.DeleteAsync(id);
        }
    }
}