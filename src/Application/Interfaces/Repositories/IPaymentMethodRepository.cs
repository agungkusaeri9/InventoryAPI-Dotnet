using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<PaginatedList<PaymentMethod>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);
        Task<PaymentMethod?> GetByIdAsync(int id);
        Task<PaymentMethod> CreateAsync(PaymentMethod paymentMethod);
        Task<PaymentMethod> UpdateAsync(PaymentMethod paymentMethod);
        Task<PaymentMethod> DeleteAsync(PaymentMethod paymentMethod);
    }
}