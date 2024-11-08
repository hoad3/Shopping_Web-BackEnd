using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Models;

namespace Web_2.AuthSetup;

public class InfoUserService:IInfoUserService
{
    private readonly AppDbContext _context;

    public InfoUserService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<InformationUser> AddInformationAsync(InformationUserChange informationuc)
    {
        var existingInformation = await _context.InformationUser.FirstOrDefaultAsync(i => i.User_id == informationuc.User_id);
        if (existingInformation != null)
        {
            return null; // Thông tin người dùng đã tồn tại
        }

        var informationUser = new InformationUser()
        {
            Idname = informationuc.Idname,
            User_id = informationuc.User_id,
            Username = informationuc.Username,
            Phone = informationuc.Phone,
            Email = informationuc.Email,
            Address = informationuc.Address
        };

        await _context.InformationUser.AddAsync(informationUser);
        await _context.SaveChangesAsync();
        
        return informationUser; // Thêm thông tin thành công
    }

    public async Task<InformationUser> FindIformationUserAsync(int id_user)
    {
        var findinformation = await (from i in _context.InformationUser where i.User_id == id_user select i)
            .FirstOrDefaultAsync();
        if (findinformation == null)
        {
            return null;
        }

        return findinformation;
    }

    public async Task<InformationUser> UpdateInformationAsync(InformationUserChange informationuc)
    {
        var existingUser = await _context.InformationUser.FirstOrDefaultAsync(i => i.User_id == informationuc.User_id);
        if (existingUser == null)
        {
            return null;
        }

        existingUser.Idname = informationuc.Idname;
        existingUser.Username = informationuc.Username;
        existingUser.Phone = informationuc.Phone;
        existingUser.Email = informationuc.Email;
        existingUser.Address = informationuc.Address;

        _context.InformationUser.Update(existingUser);
        await _context.SaveChangesAsync();

        return existingUser;
    }
    
}