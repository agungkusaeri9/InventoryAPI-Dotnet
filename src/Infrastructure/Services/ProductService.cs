using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.Product;
using InventoryApi_Dotnet.src.Application.DTOs.Unit;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using InventoryApi_Dotnet.src.Domain.Entities;
using InventoryAPI_Dotnet.src.Application.DTOs.Category;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageService _imageService;
        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IImageService imageService)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            _imageService = imageService;
        }
        public async Task<PaginatedList<ProductDTO>> GetAllAsync(int pageIndex, int pageSize, string? keyword)
        {
            var result = await _productRepository.GetAllAsync(pageIndex, pageSize, keyword);

            var items = result.Items.Select(product => new ProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Category = new CategoryDTO
                {
                    Id = product.Category.Id,
                    Code = product.Category.Code,
                    Name = product.Category.Name
                },
                Unit = new UnitDTO
                {
                    Id = product.Unit.Id,
                    Code = product.Unit.Code,
                    Name = product.Unit.Name
                },
                Stock = product.Stock,
                Price = product.Price,
                Image = _imageService.GetImageUrl(product.Image)
            }).ToList();

            return new PaginatedList<ProductDTO>(items, result.PageIndex, result.TotalPages, result.Total);
        }


        public async Task<ProductDTO?> CreateAsync(CreateProductDTO dto, IFormFile? imageFile)
        {
            string? imagePath = null;

            if (imageFile != null)
            {
                imagePath = await _imageService.UploadImageAsync(imageFile, "images/products");
            }
            var product = new Product
            {
                Code = dto.Code,
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                UnitId = dto.UnitId,
                Stock = dto.Stock,
                Price = dto.Price,
                Image = imagePath,
                CreatedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "unknown"
            };

            var created = await _productRepository.CreateAsync(product);

            return new ProductDTO
            {
                Id = created!.Id,
                Code = created.Code,
                Name = created.Name,
                Description = created.Description,
                Stock = created.Stock,
                Price = created.Price
            };
        }
    }
}