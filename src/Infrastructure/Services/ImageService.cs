using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile, string folderName)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Invalid image file");

            var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(rootPath, folderName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var extension = Path.GetExtension(imageFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }

       public string GetImageUrl(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return string.Empty;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/{fileName}";
        }
    }
}