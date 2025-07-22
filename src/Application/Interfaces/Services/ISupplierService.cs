using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Supplier;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<PaginatedList<SupplierDTO>> GetAllAsync(int pageIndex, int pageSize, string? keyword = null);
        Task<SupplierDTO?> GetByIdAsync(int id);
        Task<SupplierDTO?> GetByCodeAsync(string code);
        Task<SupplierDTO> CreateAsync(CreateSupplierDTO createSupplierDto);
        Task<SupplierDTO> UpdateAsync(int id, UpdateSupplierDTO updateSupplierDto);
        Task DeleteAsync(int id);
    }
}