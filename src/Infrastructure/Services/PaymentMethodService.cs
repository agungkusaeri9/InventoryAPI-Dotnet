using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.PaymentMethod;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<PaginatedList<PaymentMethodDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            var result = await _paymentMethodRepository.GetAllAsync(pageIndex, pageSize, keyword);
            var items = result.Items.Select(PaymentMethod => new PaymentMethodDTO
            {
                Id = PaymentMethod.Id,
                Name = PaymentMethod.Name,
                Description = PaymentMethod.Description
            }).ToList();
            return new PaginatedList<PaymentMethodDTO>(items, result.PageIndex, result.TotalPages, result.Total);
        }

        public async Task<PaymentMethodDTO?> GetByIdAsync(int id)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethod == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            return new PaymentMethodDTO
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name,
                Description = paymentMethod.Description
            };
        }

        public async Task<PaymentMethodDTO?> CreateAsync(CreatePaymentMethodDTO dto)
        {
            var paymentMethod = new PaymentMethod { Name = dto.Name, Description = dto.Description };
            var newPaymentMethod = await _paymentMethodRepository.CreateAsync(paymentMethod);
            return new PaymentMethodDTO
            {
                Id = newPaymentMethod.Id,
                Name = newPaymentMethod.Name,
                Description = newPaymentMethod.Description
            };
        }

        public async Task<PaymentMethodDTO?> UpdateAsync(int id, UpdatePaymentMethodDTO dto)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethod == null)
                return null!;
            paymentMethod.Name = dto.Name;
            paymentMethod.Description = dto.Description;
            var updatedPaymentMethod = await _paymentMethodRepository.UpdateAsync(paymentMethod);
            return new PaymentMethodDTO
            {
                Id = updatedPaymentMethod.Id,
                Name = updatedPaymentMethod.Name,
                Description = updatedPaymentMethod.Description
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethod == null)
                throw new KeyNotFoundException($"Payment method with ID {id} not found.");
            await _paymentMethodRepository.DeleteAsync(paymentMethod);
            return true;
        }

    }
}