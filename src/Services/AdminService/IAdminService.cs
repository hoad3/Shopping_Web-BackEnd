using Web_2.Models;

namespace Web_2.Services.AdminService;

public interface IAdminService
{
    Task<List<Models.Product.Product>> GetAllProductAsync();
    Task<List<Models.User>> GetAllUsersAsync();
    Task<bool> DeleteUserAsync(int userId);
    Task<List<User>> SearchUsersAsync(string searchTerm);
}