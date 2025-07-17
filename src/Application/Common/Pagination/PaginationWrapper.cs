namespace InventoryApi_Dotnet.src.Application.Common.Pagination
{
    public class PaginationWrapper<T>
    {
        public PaginatedList<T> Pagination { get; set; }

        public PaginationWrapper(PaginatedList<T> pagination)
        {
            Pagination = pagination;
        }
    }
}
