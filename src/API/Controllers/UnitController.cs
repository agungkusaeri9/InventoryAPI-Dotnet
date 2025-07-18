using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.API.Helpers;
using InventoryApi_Dotnet.src.Application.DTOs.Unit;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        public readonly IUnitService _unitService;
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnits(int pageIndex = 1, int pageSize = 10, [FromQuery] string? keyword = null)
        {
            try
            {

                var result = await _unitService.GetAllUnitAsync(pageIndex, pageSize, keyword);
                return ResponseFormatter.SuccessWithPagination(result, "Units retrieved successfully");
            }
            catch (Exception ex)
            {

                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromBody] CreateUnitDTO dto)
        {
            try
            {
                var result = await _unitService.CreateUnitAsync(dto);
                return ResponseFormatter.Success(result, "Unit created successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            try
            {
                var result = await _unitService.GetUnitByIdAsync(id);
                return ResponseFormatter.Success(result, "Unit retrieved successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return ResponseFormatter.Error(knfEx.Message, 404);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, [FromBody] UpdateUnitDTO dto)
        {
            try
            {
                var result = await _unitService.UpdateUnitAsync(id, dto);
                return ResponseFormatter.Success(result, "Unit updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            try
            {
                await _unitService.DeleteUnitAsync(id);
                return ResponseFormatter.Success("Unit deleted successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return ResponseFormatter.Error(knfEx.Message, 404);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Error(ex.Message);
            }
        }

    }
}