using InventoryApi_Dotnet.src.Application.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Dotnet.src.API.Helpers
{
    public class ResponseFormatter
    {
        public static ObjectResult Success<T>(T? data, string message = "Success", int code = 200)
        {
            var result = new ApiResponse<T>(true, message, data);
            return new ObjectResult(result) { StatusCode = code };
        }

        public static IActionResult SuccessWithPagination<T>(PaginatedList<T> paginatedList, string message = "Success", int statusCode = 200)
        {
            var response = new
            {
                status = true,
                message,
                data = paginatedList.Items,
                pagination = new
                {
                    pageIndex = paginatedList.PageIndex,
                    totalPages = paginatedList.TotalPages,
                    total = paginatedList.Total,
                    hasPreviousPage = paginatedList.HasPreviousPage,
                    hasNextPage = paginatedList.HasNextPage
                }
            };

            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static ObjectResult Error(string message = "Error", int code = 400)
        {
            var result = new ApiResponse<string>(false, message, null);
            return new ObjectResult(result) { StatusCode = code }; ;
        }

    }
}
