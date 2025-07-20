using System.Text.Json;
using InventoryApi_Dotnet.src.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace InventoryApi_Dotnet.src.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

       public async Task Invoke(HttpContext context)
{
    // 1. Check if endpoint has [AllowAnonymous]
    var endpoint = context.GetEndpoint();
    if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
    {
        await _next(context); // Skip JWT validation
        return;
    }

    // 2. Extract token
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    // 3. If token missing
    if (string.IsNullOrEmpty(token))
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = false,
            message = "Unauthorized"
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
        return;
    }

    // 4. (Opsional) Validasi token di sini...

    await _next(context);
}
 
    }
}
