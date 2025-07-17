namespace InventoryApi_Dotnet.src.Application.Common.Pagination
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int Total { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int pageIndex, int totalPages, int total)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = totalPages;
            Total = total;
        }
    }
}
