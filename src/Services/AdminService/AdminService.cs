using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Minio;
using Web_2.Models;

namespace Web_2.Services.AdminService;

public class AdminService: IAdminService
{
    private readonly AppDbContext _context;
    private readonly IMinIOService _minIOService;

    public AdminService(AppDbContext context,IMinIOService minIOService)
    {
        _minIOService = minIOService;
        _context = context;
    }
    
    public async Task<List<Models.Product.Product>> GetAllProductAsync()
    {
       
        
        var getAllProducts = await _context.product.ToListAsync();

        foreach (var product in getAllProducts)
        {
            var imageUrl = await _minIOService.GetFileUrl(product.Image);
            product.Image = imageUrl;
        }

        return getAllProducts;
    }

    public async Task<List<Models.User>> GetAllUsersAsync()
    {
        var getAllUsers = await _context.USER.ToListAsync();
        
        return getAllUsers;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _context.USER.FindAsync(userId);
        if (user == null)
        {
            return false; // Người dùng không tồn tại
        }

        _context.USER.Remove(user); // Xóa người dùng
        await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

        return true; // Trả về true nếu xóa thành công
    }
    
    public async Task<List<User>> SearchUsersAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<User>();
        }
        
        var user = await _context.USER
            .Where(u => u.account.Contains(searchTerm.ToLower()))
            .ToListAsync();


        return user;
    }
}