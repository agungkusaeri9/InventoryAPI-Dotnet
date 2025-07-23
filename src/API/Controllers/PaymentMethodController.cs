using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.PaymentMethod;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 1, int pageSize = 10, [FromQuery] string? keyword = null)
        {
            try
            {
                var result = await _paymentMethodService.GetAllAsync(pageIndex, pageSize, keyword);
                return ResponseFormatter.SuccessWithPagination(result, "Payment methods retrieved successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentMethodDTO dto)
        {
            try
            {
                var result = await _paymentMethodService.CreateAsync(dto);
                return ResponseFormatter.Success(result, "Payment method created successfully");
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
                var result = await _paymentMethodService.GetByIdAsync(id);
                return ResponseFormatter.Success(result, "Payment method retrieved successfully");
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentMethodDTO dto)
        {
            try
            {
                var result = await _paymentMethodService.UpdateAsync(id, dto);
                return ResponseFormatter.Success(result, "Payment method updated successfully");
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
                await _paymentMethodService.DeleteAsync(id);
                return ResponseFormatter.Success(null, "Payment method deleted successfully");
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