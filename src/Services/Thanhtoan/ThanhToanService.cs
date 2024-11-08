using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Minio;
using Web_2.Models.Thanhtoan;

namespace Web_2.Services.Thanhtoan;

    public class ThanhToanService: IThanhToanService
    {
        private readonly AppDbContext _context;
        // private IThanhToanService _thanhToanServiceImplementation;
        private IMinIOService _minioService;

        public ThanhToanService(AppDbContext context, IMinIOService minioService)
        {
            _minioService = minioService;
            _context = context;
        }

        // Hàm tính tổng tiền
        public int CalculateTongTien(int dongia)
        {
            return dongia;
        }

        // Hàm tạo thanh toán
        public async Task<ThanhToan> CreateThanhtoanAsync(ThanhtoanDto thanhtoanDto)
        {
            // Tính tổng tiền
            int tongtien = CalculateTongTien(thanhtoanDto.Dongia);
            // Tạo đối tượng ThanhToan mới
            var thanhtoan = new ThanhToan
            {
                Idnguoimua = thanhtoanDto.Idnguoimua,
                Idnguoiban = thanhtoanDto.Idnguoiban,
                ProductId = thanhtoanDto.ProductId,
                Soluong = thanhtoanDto.Soluong,
                Dongia = thanhtoanDto.Dongia,
                Tongtien = thanhtoanDto.Dongia * thanhtoanDto.Soluong,
                Ngaythanhtoan = thanhtoanDto.Ngaythanhtoan != default(DateTime) ? thanhtoanDto.Ngaythanhtoan : DateTime.UtcNow,
                trangthaidonhang = thanhtoanDto.trangthaidonhang,
                nguoimua = thanhtoanDto.nguoimua,
                nguoiban = thanhtoanDto.nguoiban,
                
            };

            // Thêm thanh toán vào cơ sở dữ liệu
            _context.ThanhToan.Add(thanhtoan);
            await _context.SaveChangesAsync();

            return thanhtoan;
        }

        public async Task<List<Models.Thanhtoan.ThanhToan>> GetDonhangNguoibanAsync(int userid)
        {
            var Payment = await _context.ThanhToan
                .Include(t => t.Product)
                .AsNoTracking() 
                .Where(t => t.Idnguoiban == userid)
                .ToListAsync();

            foreach (var thanhtoan in Payment)
            {
                
                if (!string.IsNullOrEmpty(thanhtoan.Product.Image))
                {
                    var imgurl = await _minioService.GetFileUrl(thanhtoan.Product.Image);
                    thanhtoan.Product.Image = imgurl;
                }
            }
            
            return Payment;
        }

        public async Task<bool> UpdateTrangThaiDonHangAsync(int id, [FromBody] int trangthaidonhang)
        {
            // Tìm đối tượng ThanhToan trong database dựa vào Id
            var thanhToan = await _context.ThanhToan.FindAsync(id);

            if (thanhToan == null)
            {
                return  false;
            }

            // Cập nhật thuộc tính trangthaidonhang
            thanhToan.trangthaidonhang = trangthaidonhang;

            // Lưu thay đổi vào database
            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Order status updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
           
        }
        
    
    
    

    public int CalculateTongTien(int soluong, int dongia)
    {
        throw new NotImplementedException();
    }
}