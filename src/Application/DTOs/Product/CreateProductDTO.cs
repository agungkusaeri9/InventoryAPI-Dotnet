using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi_Dotnet.src.Application.DTOs.Product
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "UnitId is required")]
        public int UnitId { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
    }
}