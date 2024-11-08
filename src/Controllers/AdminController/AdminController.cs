using Microsoft.AspNetCore.Mvc;
using Web_2.Models;
using Web_2.Models.Product;
using Web_2.Services.AdminService;
using Web_2.Services.Product;

namespace Web_2.Controllers.AdminController;

public class AdminController: ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IProductService _productService;

    public AdminController(IAdminService adminService, IProductService productService)
    {
        _productService = productService;
        _adminService = adminService;
    }

    [HttpGet]
    [Route("/admin/product")]
    public async Task<IActionResult> ListProducts()
    {
        var product = await _adminService.GetAllProductAsync();
        
        return Ok(product);
    }

    [HttpGet]
    [Route("/admin/user/")]
    public async Task<IActionResult> ListUsers()
    {
        var users = await _adminService.GetAllUsersAsync();
        
        return Ok(users);
    }

    [HttpDelete]
    [Route("/admin/delete/product/{prodictid}")]
    public async Task<IActionResult> DeleteProduct(int prodictid)
    {
        var product = await _productService.GetProductByIdAsync(prodictid);
        if (product == null)
        {
            return NotFound("Khong co hang hoa");
        }
        
        return Ok(product);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _adminService.DeleteUserAsync(id);
        if (!result)
        {
            return NotFound(); // Trả về 404 nếu không tìm thấy người dùng
        }

        return NoContent(); // Trả về 204 nếu xóa thành công
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<List<Product>>> Search(string term)
    {
        var products = await _productService.SearchProductAsync(term);
        return Ok(products);
    }

    [HttpGet("searchUser/{term}")]
    public async Task<ActionResult<List<User>>> SearchUser(string term)
    {
        var user = await _adminService.SearchUsersAsync(term);
        return Ok(user);
    }
    
}