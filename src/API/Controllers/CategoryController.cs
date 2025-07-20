using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.Category;
using InventoryAPI_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 1, int pageSize = 10, [FromQuery] string? keyword = null)
        {
            try
            {
                var result = await _categoryService.GetAllAsync(pageIndex, pageSize, keyword);
                return ResponseFormatter.SuccessWithPagination(result, "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                var result = await _categoryService.CreateAsync(dto);
                return ResponseFormatter.Success(result, "Category created successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);
                return ResponseFormatter.Success(result, "Category retrieved successfully");
            }
            catch (System.Exception ex)
            {

                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCategoryDTO dto)
        {
            try
            {
                var result = await _categoryService.UpdateAsync(id, dto);
                return ResponseFormatter.Success(result, "Category updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return ResponseFormatter.Success(null, "Category deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }
    }
}