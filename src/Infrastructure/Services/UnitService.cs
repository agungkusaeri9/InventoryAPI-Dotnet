using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Unit;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public async Task<PaginatedList<UnitDTO>> GetAllUnitAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            var result = await _unitRepository.GetAllUnitsAsync(pageIndex, pageSize, keyword);
            return result;

        }


        public async Task<UnitDTO> CreateUnitAsync(CreateUnitDTO dto)
        {
            var checkData = await _unitRepository.GetUnitByCodeAsync(dto);
            if (checkData != null)
            {
                throw new Exception("Unit with this code already exists.");
            }
            var createdUnit = await _unitRepository.CreateUnitAsync(dto);
            return createdUnit;
        }

        public async Task<UnitDTO> GetUnitByIdAsync(int id)
        {
            var unit = await _unitRepository.GetUnitByIdAsync(id);
            if (unit == null)
                throw new KeyNotFoundException("Unit not found.");
            else
            {
                return unit;
            }
        }

        public async Task<UnitDTO?> UpdateUnitAsync(int id, UpdateUnitDTO dto)
        {
            var existingUnit = await _unitRepository.GetUnitByIdAsync(id);
            if (existingUnit == null)
            {
                throw new KeyNotFoundException("Unit not found.");
            }
            var unit = await _unitRepository.UpdateUnitAsync(id, dto);
            if (unit == null)
            {
                throw new Exception("Failed to update unit.");
            }
            return unit;

        }

        public async Task<bool> DeleteUnitAsync(int id)
        {
            var deletedUnit = await _unitRepository.DeleteUnitAsync(id);
            if (!deletedUnit)
            {
                throw new KeyNotFoundException("Unit not found.");
            }
            return deletedUnit;
        }
    }
}

