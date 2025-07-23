using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.PaymentMethod;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentMethodRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<PaymentMethod>> GetAllAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var query = _context.PaymentMethods.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            var items = await query.OrderBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new PaymentMethod { Id = x.Id, Name = x.Name, Description = x.Description }).ToListAsync();
            return new PaginatedList<PaymentMethod>(items, pageIndex, totalPages, count);
        }

        public async Task<PaymentMethod?> GetByIdAsync(int id)
        {
            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.Id == id);
            if (paymentMethod == null) return null;
            return paymentMethod;
        }
        public async Task<PaymentMethod> CreateAsync(PaymentMethod paymentMethod)
        {
            await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

        public async Task<PaymentMethod> UpdateAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

        public async Task<PaymentMethod> DeleteAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

    }
}