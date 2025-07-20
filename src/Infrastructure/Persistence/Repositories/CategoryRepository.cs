using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Category;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryAPI_Dotnet.src.Application.DTOs.Category;
using InventoryAPI_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<CategoryDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Code.Contains(keyword) || x.Name.Contains(keyword));
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var items = await query.OrderBy(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                })
                .ToListAsync();

            return new PaginatedList<CategoryDTO>(items, pageIndex, totalPages, count);
        }

        public async Task<CategoryDTO?> CreateAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Code = dto.Code
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Code = category.Code
            };
        }

        public async Task<CategoryDTO?> GetByCodeAsync(string code)
        {
            var category = await _context.Categories
                .Where(x => x.Code == code)
                .Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                })
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.Where(x => x.Id == id).Select(x => new CategoryDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            }).FirstOrDefaultAsync();
            if (category == null)
                return null;
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Code = category.Code
            };
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;
            category.Name = dto.Name;
            category.Code = dto.Code;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Code = category.Code
            };
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}