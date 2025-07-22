using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Supplier;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using InventoryApi_Dotnet.src.Domain.Entities;
using InventoryApi_Dotnet.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<SupplierDTO>> GetAllAsync(int pageIndex = 1, int pageSize = 10, string? keyword = null)
        {
            var result = await _repository.GetAllAsync(pageIndex, pageSize, keyword);
            var items = result.Items.Select(supplier => new SupplierDTO
            {
                Id = supplier.Id,
                Code = supplier.Code,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                ContactPerson = supplier.ContactPerson
            }).ToList();

            return new PaginatedList<SupplierDTO>(items, result.PageIndex, result.TotalPages, result.Total);
        }

        public async Task<SupplierDTO?> GetByIdAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null) return null;

            return new SupplierDTO
            {
                Id = supplier.Id,
                Code = supplier.Code,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                ContactPerson = supplier.ContactPerson
            };
        }

        public async Task<SupplierDTO?> GetByCodeAsync(string code)
        {
            var supplier = await _repository.GetByCodeAsync(code);
            if (supplier == null) return null;

            return new SupplierDTO
            {
                Id = supplier.Id,
                Code = supplier.Code,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                ContactPerson = supplier.ContactPerson
            };
        }

        public async Task<SupplierDTO> CreateAsync(CreateSupplierDTO createSupplierDto)
        {
            var existingSupplier = await _repository.GetByCodeAsync(createSupplierDto.Code);
            if (existingSupplier != null)
            {
                throw new ArgumentException($"Supplier with code {createSupplierDto.Code} already exists.");
            }

            var supplier = new Supplier
            {
                Code = createSupplierDto.Code,
                Name = createSupplierDto.Name,
                Address = createSupplierDto.Address,
                Phone = createSupplierDto.Phone,
                Email = createSupplierDto.Email,
                ContactPerson = createSupplierDto.ContactPerson
            };

            var createdSupplier = await _repository.CreateAsync(supplier);
            return new SupplierDTO
            {
                Id = createdSupplier.Id,
                Code = createdSupplier.Code,
                Name = createdSupplier.Name,
                Address = createdSupplier.Address,
                Phone = createdSupplier.Phone,
                Email = createdSupplier.Email,
                ContactPerson = createdSupplier.ContactPerson
            };
        }

        public async Task<SupplierDTO> UpdateAsync(int id, UpdateSupplierDTO updateSupplierDto)
        {
            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }

            var existingCode = await _repository.GetByCodeAsync(updateSupplierDto.Code);
            if (existingCode != null && existingCode.Id != id)
            {
                throw new ArgumentException($"Supplier with code {updateSupplierDto.Code} already exists.");
            }

            supplier.Code = updateSupplierDto.Code;
            supplier.Name = updateSupplierDto.Name;
            supplier.Address = updateSupplierDto.Address;
            supplier.Phone = updateSupplierDto.Phone;
            supplier.Email = updateSupplierDto.Email;
            supplier.ContactPerson = updateSupplierDto.ContactPerson;

            var updatedSupplier = await _repository.UpdateAsync(supplier);
            return new SupplierDTO
            {
                Id = updatedSupplier.Id,
                Code = updatedSupplier.Code,
                Name = updatedSupplier.Name,
                Address = updatedSupplier.Address,
                Phone = updatedSupplier.Phone,
                Email = updatedSupplier.Email,
                ContactPerson = updatedSupplier.ContactPerson
            };
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null) throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            await _repository.DeleteAsync(id);
        }
    }
}