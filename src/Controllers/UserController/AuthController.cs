using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web_2.AuthSetup;
using Web_2.Data;
using Web_2.Models;

namespace Web_2.Controllers;


[ApiController]
// [Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // private readonly AppDbContext _context;
    private readonly AuthService _authService;
    // private readonly TokenService _tokenService;
    private readonly IUserService _userService;

    public AuthController( IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegisterDto user)
    {
        
            var result = await _userService.RegisterUserAsync(user);
            return Ok(result);
        
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserRegisterDto user)
    {
        
        try
        {
            var (token, dbUser) = await _userService.LoginUserAsync(user);
            return Ok(new
            {
                Token = token,
                Userid = dbUser.id,
                Role = dbUser.role, // Cũng có thể trả về role nếu cần
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred during login.");
        }
    }
    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword(string email, string password)
    {
        // Kiểm tra sự tồn tại của account
        var existingUser = await _userService.ChangePasswordAsync(email, password);
        if (existingUser == null)
        {
            return NotFound("Account does not exist.");
        }

        return Ok("Password changed successfully");
    }

    [HttpGet]
    [Route("Find_Account/{userId}")]
    public async Task<IActionResult> FindAccount(int userId)
    {
        var dbUser = await _userService.FindAccountAsync(userId);
        if (dbUser == null)
        {
            return Unauthorized("Invalid credentials");
        }

        return Ok(dbUser);
    }

    [HttpGet]
    [Route("Get_Account")]
    public async Task<IActionResult> GetAllAccounts()
    {
        var account = _userService.GetAllUsersAsync();
        
        return Ok(account);
    }

    [HttpPost]
    [Route("Delete_Account/{userId}")]
    public async Task<IActionResult> DeleteAccount(int userId)
    {
        var delete = _userService.DeleteUserByIdAsync(userId);
        
        return Ok(delete);
    }
}
