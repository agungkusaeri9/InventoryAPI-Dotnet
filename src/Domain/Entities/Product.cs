using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InventoryAPI_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Precision(18,2)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public int Stock { get; set; }
        public string? Image { get; set; } = string.Empty;
    }
}