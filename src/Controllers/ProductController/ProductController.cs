using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Migrations;
using Web_2.Minio;
using Web_2.Models.Product;
using Web_2.Services.Product;

namespace Web_2.Controllers.ProductController;


[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController( IProductService iproductService)
    {
        _productService = iproductService;
    }
    
    [HttpPut]
    [Route("AddProduct")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddProduct([FromForm] ProductCreateDto productDto)
    {
        var product = await _productService.AddProductAsync(productDto);
     
        // Kiểm tra nếu không có file ảnh được upload
        if (productDto.image == null || productDto.image.Length == 0)
        {
            return BadRequest("Không có file ảnh được upload.");
        }

        if (product == null)
        {
            return NotFound("Khong add duoc hang hoa");
        }
        
        return Ok(product);
    
    }


    [HttpGet]
    [Route("Find_a_Product")]
    public async Task<IActionResult> FindProduct(int id)
    {
        
        var findResult = await _productService.GetProductByIdAsync(id);
        if (findResult == null)
        {
            return NotFound("Khong co hang hoa");
        }
        
        return Ok(findResult);
    }

    [HttpGet]
    [Route("Find_Product_UserId/{sellerId}")]
    public async Task<IActionResult> FindProductUserid(int sellerId)
    {
        
        var products = await _productService.GetProductByUserIdAsync(sellerId);
        if (products == null)
        {
            return NotFound("Khong co product");
        }
        return Ok(products);
    }

    [HttpGet]
    [Route("Get_All_Produc")]
    public async Task<IActionResult> GetAllProduct(int page = 1, int limit = 4)
    {
        
        var products = await _productService.GetAllProductAsync(page, limit);

        if (products == null || products.Count == 0)
        {
            return NotFound("khong co san pham nao");
        }

        return Ok(products);
    }
    [HttpDelete]
    [Route("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var findProduct = await _productService.GetProductByIdAsync(id);
        if (findProduct == null)
        {
            return NotFound("Khong co hang hoa");
        }
        
        return Ok(findProduct);
    }

    [HttpPatch]
    [Route("ChangeProduct/{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ChangeProduct(int id, [FromForm] ProductCreateDto productdto)
    {
        var findAsync = await _productService.ChangeProductAsync(id, productdto);
        if (findAsync == null)
        {
            return NotFound(new { Message = "Product not found" });
        }

        return Ok(findAsync);

    }
    
}
