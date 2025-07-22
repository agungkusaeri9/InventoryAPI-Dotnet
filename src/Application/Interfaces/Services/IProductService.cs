using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Product;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<PaginatedList<ProductDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);
        // Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO?> CreateAsync(CreateProductDTO createProductDTO, IFormFile imageFile);
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<ProductDTO?> UpdateAsync(int id, UpdateProductDTO updateProductDTO, IFormFile? imageFile);
        Task<bool> DeleteAsync(int id);
    }
}