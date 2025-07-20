namespace InventoryAPI_Dotnet.src.Application.DTOs.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set;} = string.Empty;
    }
}