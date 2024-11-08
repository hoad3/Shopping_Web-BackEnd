using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Models.Thanhtoan;
using Web_2.Services.Donmua;
using Web_2.Services.Thanhtoan;

namespace Web_2.Controllers;

public class ThanhtoanController: ControllerBase
{
    private readonly IThanhToanService _thanhtoanService;
    // private readonly AppDbContext _context;
    private readonly IDonmuaService _donmuaService;

    public ThanhtoanController(IThanhToanService thanhtoanService, IDonmuaService donmuaService)
    {
        _thanhtoanService = thanhtoanService;
        // _context = context;
        _donmuaService = donmuaService;
   
    }

    [HttpPost]
    [Route("Thanhtoan")]
    public async Task<IActionResult> CreateThanhtoan([FromBody] ThanhtoanDto thanhtoanDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Sử dụng service để tạo thanh toán
        var thanhtoan = await _thanhtoanService.CreateThanhtoanAsync(thanhtoanDto);

        return Ok(new { message = "Thanh toán đã được tạo thành công", thanhtoan });
    }
    
    [HttpPost]
    [Route("Donmua")]
    public async Task<IActionResult> CreateDonmua([FromBody] DonmuaCreateDto donmuaDto)
    {
        try
        {
            var donmua = await _donmuaService.CreateDonmuaAsync(donmuaDto);
            return CreatedAtAction(nameof(GetDonmuaByIdAsync), new { id = donmua.Iddonmua }, donmua);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("Get_donmua_userID/{userid}")]
    public async Task<IActionResult> GetbyUserId(int userid)
    {
        var findresult = await _donmuaService.GetDonmuaByUserIdAsync(userid); 
        if (findresult == null)
        {
            return NotFound("khong co don mua"); // Trả về 404 nếu không tìm thấy đơn mua
        }
        return Ok(findresult); // Trả về 200 OK với danh sách đơn mua
    }

    [HttpGet]
    [Route("Get_donmua_id/{id}")]
    public async Task<IActionResult> GetDonmuaByIdAsync(int id)
    {
        var findresult = await _donmuaService.GetDonmuaByIdAsync(id);
        if (findresult == null)
        {
            return NotFound("khong tim thay don mua");
        }
        return Ok(findresult);
    }

    [HttpGet]
    [Route("Get_ThanhToan/{idnguoiban}")]
    public async Task<IActionResult> GetThanhToan(int idnguoiban)
    {
        var nguoiban = await _thanhtoanService.GetDonhangNguoibanAsync(idnguoiban);
        if (nguoiban == null)
        {
            return BadRequest("khong co don hang");
        }

        return Ok(nguoiban);
    }
    
    [HttpPatch("UpdateTrangThaiDonHang/{id}")]
    public async Task<IActionResult> UpdateTrangThaiDonHang(int id, [FromBody] int trangthaidonhang)
    {
        var result = await _thanhtoanService.UpdateTrangThaiDonHangAsync(id, trangthaidonhang);

        if (!result)
        {
            return NotFound(new { message = "ThanhToan not found or update failed" });
        }

        return Ok(new { message = "Order status updated successfully" });
    }

    [HttpPost("UpdateThanhtoanState")]
    public async Task<IActionResult> UpdateThanhtoanState(int Id)
    {
        var result = await _donmuaService.UpdateThanhtoanState(Id);

        if (!result)
        {
            return NotFound(new { message = "ThanhToan not found or update failed" });
        }
        
        return Ok(new { message = "Order status updated successfully" });
        
    }

    [HttpGet("Get_Donmua_Nguoimua/{nguoimuaid}")]
    public async Task<IActionResult> GetDonmuaNguoimua(int nguoimuaid)
    {
        var result = await _donmuaService.GetDonmuaBynguoimuaAsync(nguoimuaid);

        if (result == null)
        {
            return NotFound(new { message = "Donmua not found" });
        }
        return Ok(result);
    }
    
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetDonmuaById(int id)
    // {
    //     var donmua = await _context.Donmua.FindAsync(id);
    //     if (donmua == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(donmua);
    // }
}