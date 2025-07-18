namespace InventoryApi_Dotnet.src.API.Helpers
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponse(bool status, string message, T? data = default)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
