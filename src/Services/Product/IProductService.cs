using Microsoft.AspNetCore.Mvc;
using Web_2.Models.Product;

namespace Web_2.Services.Product;

public interface IProductService
{
    Task<List<Models.Product.Product>> GetAllProductAsync(int page, int limit);
    Task<Models.Product.Product> GetProductByIdAsync(int id);
    Task<List<Models.Product.Product>> GetProductByUserIdAsync(int userId);
    Task<Models.Product.Product> AddProductAsync([FromForm] ProductCreateDto productDto);
    Task<Models.Product.Product> ChangeProductAsync(int id, [FromForm] ProductCreateDto productdto);
    Task<List<Models.Product.Product>> SearchProductAsync(string searchTerm);
}