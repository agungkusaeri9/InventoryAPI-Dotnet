using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi_Dotnet.src.Application.DTOs.Supplier
{
    public class CreateSupplierDTO
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? ContactPerson { get; set; } = string.Empty;

    }
}