using Microsoft.AspNetCore.Mvc;
using Web_2.Models.Thanhtoan;

namespace Web_2.Services.Thanhtoan;

public interface IThanhToanService
{
    Task<ThanhToan> CreateThanhtoanAsync(ThanhtoanDto thanhtoanDto);
    Task<List<Models.Thanhtoan.ThanhToan>> GetDonhangNguoibanAsync(int userid);
    Task<bool> UpdateTrangThaiDonHangAsync(int id, [FromBody] int trangthaidonhang);
    int CalculateTongTien(int soluong, int dongia);
}