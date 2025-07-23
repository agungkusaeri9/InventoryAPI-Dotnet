using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName);
        string GetImageUrl(string? image);
        Task<bool> DeleteImageAsync(string imagePath);
    }
}