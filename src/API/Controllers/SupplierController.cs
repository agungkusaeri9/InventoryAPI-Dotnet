using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.Supplier;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 1, int pageSize = 10, [FromQuery] string? keyword = null)
        {
            try
            {
                var result = await _supplierService.GetAllAsync(pageIndex, pageSize, keyword);
                return ResponseFormatter.SuccessWithPagination(result, "Suppliers retrieved successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                return ResponseFormatter.Success(supplier, "Supplier retrieved successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return ResponseFormatter.Error(knfEx.Message, 403);
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDTO dto)
        {
            try
            {
                var supplier = await _supplierService.CreateAsync(dto);
                return ResponseFormatter.Success(supplier, "Supplier created successfully", 201);
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierDTO dto)
        {
            try
            {
                var supplier = await _supplierService.UpdateAsync(id, dto);
                return ResponseFormatter.Success(supplier, "Supplier updated successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return ResponseFormatter.Error(knfEx.Message, 404);
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _supplierService.DeleteAsync(id);
                return ResponseFormatter.Success(null, "Supplier deleted successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return ResponseFormatter.Error(knfEx.Message, 404);
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

    }
}