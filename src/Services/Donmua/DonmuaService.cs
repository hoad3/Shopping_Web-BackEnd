using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Web_2.Data;
using Web_2.Minio;
using Web_2.Models.Carts;
using Web_2.Models.Thanhtoan;

namespace Web_2.Services.Donmua;

public class DonmuaService: Models.Thanhtoan.Donmua, IDonmuaService
{
    private readonly AppDbContext _context;
    private readonly IMinIOService _ioService;
    private readonly IMemoryCache _cache;
    
    public DonmuaService(AppDbContext context, IMinIOService ioService,IMemoryCache cache)
    {
        _cache = cache;
        _ioService = ioService;
        _context = context;
       
    }
    
    public async Task<Models.Thanhtoan.Donmua> CreateDonmuaAsync(DonmuaCreateDto donmuaDto)
    {
        if (donmuaDto == null || donmuaDto.Idnguoiban <= 0 || donmuaDto.Idnguoimua <= 0 || donmuaDto.Idproduct <= 0 || donmuaDto.Ngaydat == default)
        {
            throw new ArgumentException("Missing or invalid data.");
        }
        // Lấy thông tin người mua và người bán từ bảng InformationUser
        var usersInfo = await _context.InformationUser
            .Where(i => i.User_id == donmuaDto.Idnguoimua || i.User_id == donmuaDto.Idnguoiban)
            .ToListAsync();
        
        var nguoimuaInfo = usersInfo.FirstOrDefault(i => i.User_id == donmuaDto.Idnguoimua);
        if (nguoimuaInfo == null)
        {
            throw new InvalidOperationException("Người mua không tồn tại.");
        }

        var nguoibanInfo = usersInfo.FirstOrDefault(i => i.User_id == donmuaDto.Idnguoiban);
        if (nguoibanInfo == null)
        {
            throw new InvalidOperationException("Người bán không tồn tại.");
        }
        
        var donmua = new Models.Thanhtoan.Donmua
        {
            idnguoiban = donmuaDto.Idnguoiban,
            idnguoimua = donmuaDto.Idnguoimua,
            idproduct = donmuaDto.Idproduct,
            ngaydat = donmuaDto.Ngaydat,
            dongia = donmuaDto.dongia,
            soluong = donmuaDto.soluong,
            name = donmuaDto.name,
            tongtien = donmuaDto.tongtien,
            nguoimua = nguoimuaInfo.Username,
            nguoiban = nguoibanInfo.Username,
            phuongthucthanhtoan = donmuaDto.phuongthucthanhtoan,
            trangthaithanhtoan = donmuaDto.trangthaithanhtoan
        };
    
        await _context.Donmua.AddAsync(donmua);
    
        var product = await _context.product.FindAsync(donmuaDto.Idproduct);
    
        if (product != null)
        {
            if (product.Stockquantity >= donmuaDto.soluong)
            {
                product.Stockquantity -= donmuaDto.soluong;
    
                _context.product.Update(product);
            }
            else
            {
                throw new InvalidOperationException("Số lượng sản phẩm không đủ để thực hiện thêm đơn hàng.");
            }
        }
        else
        {
            throw new InvalidOperationException("Sản phẩm không tồn tại.");
        }
    
        var cartItem = await _context.CartItemShoping.FirstOrDefaultAsync(c => c.ProductId == donmuaDto.Idproduct);
        if (cartItem != null)
        {
            _context.CartItemShoping.Remove(cartItem);
        }
        
        await _context.SaveChangesAsync();
        
        return donmua;
    }
    
    public async Task<List<Models.Thanhtoan.Donmua>> GetDonmuaByUserIdAsync(int userid)
    {
        var findresult = await _context.Donmua
            .Include(i => i.Product)
            .AsNoTracking() 
            .Where(d =>d.idnguoiban == userid)
            .ToListAsync();
        if (findresult == null || findresult.Count == 0)
        {
            
            Console.WriteLine("Khong co don hang");
        }

        foreach (var donmua in findresult)
        {
            if (!string.IsNullOrEmpty(donmua.Product.Image))
            {
                donmua.Product.Image = await _ioService.GetFileUrl(donmua.Product.Image);
            }
        }

        return findresult;
    }

    public async Task<bool> UpdateThanhtoanState(int Id)
    {
        var donmuaList  = await _context.Donmua
            .Where(d => d.idnguoimua == Id && d.phuongthucthanhtoan == 2)
            .ToListAsync();

        if (donmuaList == null || donmuaList.Count == 0)
        {
            // Không tìm thấy đơn mua nào cần cập nhật
            return false;
        }

        // Cập nhật trạng thái thanh toán của các đơn mua tìm được thành 2
        foreach (var donmua in donmuaList)
        {
            donmua.trangthaithanhtoan = 2;
        }

        try
        {
            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            Console.WriteLine("Order status updated successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating order status: " + ex.Message);
            return false;
        }
        
    }
    
    public async Task<List<Models.Thanhtoan.Donmua>> GetDonmuaBynguoimuaAsync(int nguoimuaId)
    {
        var findresult = await _context.Donmua
            .Include(i => i.Product)
            .AsNoTracking() 
            .Where(d =>d.idnguoimua == nguoimuaId)
            .ToListAsync();
        if (findresult == null || findresult.Count == 0)
        {
            
            Console.WriteLine("Khong co don hang");
        }

        foreach (var donmua in findresult)
        {
            if (!string.IsNullOrEmpty(donmua.Product.Image))
            {
                donmua.Product.Image = await _ioService.GetFileUrl(donmua.Product.Image);
            }
        }

        return findresult;
    }
    public async Task<Models.Thanhtoan.Donmua> GetDonmuaByIdAsync(int id)
    {
        return await _context.Donmua.FindAsync(id);
    }
    
}