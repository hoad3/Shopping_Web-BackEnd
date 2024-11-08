using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Models;
using Web_2.Models.Delivery;

namespace Web_2.AuthSetup;

public class UserService: IUserService
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public UserService(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    public async Task<string> RegisterUserAsync(UserRegisterDto user)
    {
        // Kiểm tra sự tồn tại của account
        var existingUser = await _context.USER.FirstOrDefaultAsync(u => u.account == user.account);
        if (existingUser != null)
        {
            throw new ArgumentException("Account already exists.");
        }

        // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
        var hashedPassword = HashPassword(user.password);

        var newUser = new User()
        {
            id = user.id,
            account = user.account,
            password = hashedPassword,
            role = user.role,
        };
        
        await _context.USER.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return "User registered successfully";
    }

    private string HashPassword(string password)
    {
        // Sử dụng một thuật toán băm mạnh mẽ (ví dụ: BCrypt, PBKDF2, hoặc Argon2)
        // Dưới đây là ví dụ sử dụng BCrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public async Task<(string token, User dbUser)> LoginUserAsync(UserRegisterDto user)
    {
        // Kiểm tra thông tin đăng nhập
        var dbUser = await _context.USER
            .FirstOrDefaultAsync(u => u.account == user.account);
        if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.password, dbUser.password)) // So sánh mật khẩu đã mã hóa
        {
            throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác");
        }

        // Tạo JWT token
        var token = _tokenService.GenerateToken(dbUser);

        return (token, dbUser); // Trả về token
    }

    public async Task<bool> ChangePasswordAsync(string email, string newPassword)
    {
        // var existingUser = await _context.USER.FirstOrDefaultAsync(u => u.account == user.account);
        // if (existingUser == null)
        // {
        //     return false; // Tài khoản không tồn tại
        // }
        //
        // // Mã hóa mật khẩu mới trước khi lưu vào cơ sở dữ liệu
        // existingUser.password = HashPassword(user.newPassword);
        // _context.USER.Update(existingUser);
        // await _context.SaveChangesAsync();
        //
        // return true; // Đổi mật khẩu thành công
        
        // Tìm thông tin người dùng dựa trên email
        var userInfo = await _context.InformationUser.FirstOrDefaultAsync(i => i.Email == email);
        if (userInfo == null)
        {
            return false; // Email không tồn tại
        }
        
        // Tìm người dùng dựa trên User_id từ thông tin người dùng
        var existingUser = await _context.USER.FirstOrDefaultAsync(u => u.id == userInfo.User_id);
        if (existingUser == null)
        {
            return false; // Tài khoản không tồn tại
        }
        
        // Mã hóa mật khẩu mới trước khi lưu vào cơ sở dữ liệu
        existingUser.password = HashPassword(newPassword);
        _context.USER.Update(existingUser);
        await _context.SaveChangesAsync();

        return true; // Đổi mật khẩu thành công
    }
    
    public async Task<bool> IsEmailRegisteredAsync(string email)
    {
        return await _context.InformationUser.AnyAsync(u => u.Email == email);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return _context.USER.ToList();
    }

    public async Task<User> DeleteUserByIdAsync(int id)
    {
        var existingUser = await _context.USER.FirstOrDefaultAsync(u => u.id == id);
        
        _context.USER.Remove(existingUser);
        _context.SaveChanges();
        
        return existingUser;
    }
    
    public async Task<User> FindAccountAsync(int userId)
    {
        var dbUser = await _context.USER.FirstOrDefaultAsync(u => u.id == userId);
        return dbUser; // Trả về user nếu tìm thấy, nếu không sẽ là null
    }

   
}