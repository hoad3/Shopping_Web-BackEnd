using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.AuthSetup;
using Web_2.Data;
using Web_2.Models;

namespace Web_2.Controllers;

[ApiController]
public class InformationUserController: ControllerBase
{
   
    private readonly IInfoUserService _infoUserService;
    private readonly AppDbContext _context;
    
    public InformationUserController(IInfoUserService infoUserService,AppDbContext context)
    {
        _context = context;
       
        _infoUserService = infoUserService;
    }
    
    [HttpPost]
    [Route("api/AddInformation")]
    public async Task<IActionResult> AddInformation(InformationUserChange informationuc)
    {
        var existinginformatio = await _infoUserService.AddInformationAsync(informationuc);
        if (existinginformatio != null)
        {
            return BadRequest("information user already exists.");
        }
      
        return Ok();
    }

    [HttpGet]
    [Route("API/Find_Information_User/{id_user}")]
    public async Task<IActionResult> FindIformationUser(int id_user)
    {
        var findinformation = await _infoUserService.FindIformationUserAsync(id_user);
        if (findinformation == null)
        {
            return NotFound("Khong co thong tin");
        }
        
        return Ok(findinformation);
    }
    [HttpPut]
    [Route("api/UpdateInformation")]
    public async Task<IActionResult> UpdateInformation(InformationUserChange informationuc)
    {
        var existingUser = await _infoUserService.UpdateInformationAsync(informationuc);
        if (existingUser == null)
        {
            return NotFound("Khong co thong tin");
        }
        return Ok(existingUser);
    }
    
    [HttpGet("FindByNguoiMuaId/{idnguoimua}")]
    public async Task<IActionResult> GetUserByNguoiMuaId(int idnguoimua)
    {
        var user = await _context.InformationUser
            .Where(u => u.User_id == idnguoimua)
            .Select(u => new
            {
                u.Idname,
                u.User_id,
                u.Username,
                u.Phone,
                u.Email,
                u.Address
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }
}