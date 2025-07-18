using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi_Dotnet.src.Application.DTOs.Unit
{
    public class UpdateUnitDTO
    {
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
    }
}