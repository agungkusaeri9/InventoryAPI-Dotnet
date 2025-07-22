using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task<PaginatedList<Supplier>> GetAllAsync(int pageNumber, int pageSize, string? keyword = null);
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier?> GetByCodeAsync(string code);
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(Supplier supplier);
        Task DeleteAsync(int id);
    }
}