using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryAPI_Dotnet.src.Domain.Entities
{
    public class Category : BaseEntity
    { 
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}