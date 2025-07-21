using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.DTOs.Unit;
using InventoryAPI_Dotnet.src.Application.DTOs.Category;
using InventoryAPI_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Application.DTOs.Product
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public CategoryDTO Category { get; set; } = null!;
        public UnitDTO Unit { get; set; } = null!;
        public int Stock { get; set; }
        public string? Image { get; set; } = string.Empty;
    }
}