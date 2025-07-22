using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.Product;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 1, int pageSize = 10, [FromQuery] string? keyword = null)
        {
            try
            {
                var result = await _productService.GetAllAsync(pageIndex, pageSize, keyword);
                return ResponseFormatter.SuccessWithPagination(result, "Products retrieved successfully");
            }
            catch (System.Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductDTO dto, IFormFile image)
        {
            try
            {
                var result = await _productService.CreateAsync(dto, image);
                return ResponseFormatter.Success(result, "Product created successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsyn(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);
                return ResponseFormatter.Success(result, "Product retrieved successfully");
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateProductDTO dto, IFormFile? image)
        {
            try
            {
                var result = await _productService.UpdateAsync(id, dto, image);
                return ResponseFormatter.Success(result, "Product updated successfully");
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return ResponseFormatter.Success(null, "Product deleted successfully");
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
    }
}