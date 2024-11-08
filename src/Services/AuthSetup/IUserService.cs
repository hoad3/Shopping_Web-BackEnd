using Web_2.Models;
using Web_2.Models.Delivery;

namespace Web_2.AuthSetup;

public interface IUserService
{
    Task<string> RegisterUserAsync(UserRegisterDto user);
    Task<(string token, User dbUser)> LoginUserAsync(UserRegisterDto user);
    Task<bool> ChangePasswordAsync(string email, string newPassword);
    Task<User> FindAccountAsync(int userId);
    Task<List<User>> GetAllUsersAsync();
    Task<User> DeleteUserByIdAsync(int id);

    Task<bool> IsEmailRegisteredAsync(string email);

}