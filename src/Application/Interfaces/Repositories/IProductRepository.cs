using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Product;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<PaginatedList<Product>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);
        Task<Product?> CreateAsync(Product product); 
    }
}