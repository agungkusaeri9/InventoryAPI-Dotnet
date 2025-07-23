using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<Supplier?> GetByCodeAsync(string code)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Code == code);
            return supplier;
        }

        public async Task<PaginatedList<Supplier>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s => s.Name.Contains(keyword) || s.Code.Contains(keyword) || s.Address!.Contains(keyword) || s.Phone!.Contains(keyword) || s.Email!.Contains(keyword) || s.ContactPerson!.Contains(keyword));
            }

            var totalCount = await query.CountAsync();
            var TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new Supplier
                {
                    Id = s.Id,
                    Code = s.Code,
                    Name = s.Name,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    ContactPerson = s.ContactPerson
                })
                .ToListAsync();

            return new PaginatedList<Supplier>(items, pageIndex, TotalPages, totalCount);
        }

        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await GetByIdAsync(id);
            _context.Suppliers.Remove(supplier!);
            await _context.SaveChangesAsync();
        }
    }
}