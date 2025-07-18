using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Unit;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;
        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<UnitDTO>> GetAllUnitsAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var query = _context.Units.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new UnitDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                })
                .ToListAsync();

            return new PaginatedList<UnitDTO>(items, pageIndex, totalPages, count);
        }

        // public async Task<UnitDTO> GetUnitByIdAsync(int id)
        // {
        //     var unit = await _context.Units
        //         .Where(x => x.Id == id)
        //         .Select(x => new UnitDTO
        //         {
        //             Id = x.Id,
        //             Name = x.Name,
        //             Code = x.Code
        //         })
        //         .FirstOrDefaultAsync();

        //     return new UnitDTO
        //     {
        //         Id = unit?.Id ?? 0,
        //         Name = unit?.Name ?? string.Empty,
        //         Code = unit?.Code ?? string.Empty
        //     };
        // }

        public async Task<UnitDTO> CreateUnitAsync(CreateUnitDTO dto)
        {
            var unit = new Unit
            {
                Name = dto.Name,
                Code = dto.Code
            };

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return new UnitDTO
            {
                Id = unit.Id,
                Name = unit.Name,
                Code = unit.Code
            };
        }

        public async Task<UnitDTO?> GetUnitByCodeAsync(CreateUnitDTO dto)
        {
            var unit = await _context.Units
                .Where(x => x.Code == dto.Code)
                .Select(x => new UnitDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                })
                .FirstOrDefaultAsync();

            if (unit == null)
                return null;

            return new UnitDTO
            {
                Id = unit?.Id ?? 0,
                Name = unit?.Name ?? string.Empty,
                Code = unit?.Code ?? string.Empty
            };
        }

        public async Task<UnitDTO?> GetUnitByIdAsync(int id)
        {
            var unit = await _context.Units
                .Where(x => x.Id == id)
                .Select(x => new UnitDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                })
                .FirstOrDefaultAsync();


            if (unit == null)
            {
                return null;
            }

            return new UnitDTO
            {
                Id = unit?.Id ?? 0,
                Name = unit?.Name ?? string.Empty,
                Code = unit?.Code ?? string.Empty
            };
        }


        public async Task<UnitDTO?> UpdateUnitAsync(int id, UpdateUnitDTO dto)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null) return null;

            unit.Name = dto.Name;
            unit.Code = dto.Code;

            _context.Units.Update(unit);
            await _context.SaveChangesAsync();

            return new UnitDTO
            {
                Id = unit.Id,
                Name = unit.Name,
                Code = unit.Code
            };
        }

        public async Task<bool> DeleteUnitAsync(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null) return false;

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}