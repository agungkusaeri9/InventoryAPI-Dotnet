using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Unit;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IUnitService
    {
        Task<PaginatedList<UnitDTO>> GetAllUnitAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);

        // Task<UnitDTO> GetUnitByIdAsync(int id);
        Task<UnitDTO> CreateUnitAsync(CreateUnitDTO dto);
        Task<UnitDTO> GetUnitByIdAsync(int id);
        Task<UnitDTO?> UpdateUnitAsync(int id, UpdateUnitDTO dto);
        Task<bool> DeleteUnitAsync(int id);
    }
}