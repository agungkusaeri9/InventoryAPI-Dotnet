using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.PaymentMethod;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IPaymentMethodService
    {
        Task<PaginatedList<PaymentMethodDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null);
        Task<PaymentMethodDTO?> GetByIdAsync(int id);
        Task<PaymentMethodDTO?> CreateAsync(CreatePaymentMethodDTO dto);
        Task<PaymentMethodDTO?> UpdateAsync(int id, UpdatePaymentMethodDTO dto);
        Task<bool> DeleteAsync(int id);


    }
}