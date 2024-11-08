using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Models.Delivery;

namespace Web_2.Services.DeliveryService;

public class DeliverySevice: IDeliveryService
{   
    private readonly AppDbContext _context;

    public DeliverySevice(AppDbContext context)
    {
        _context = context;
    }

    public async Task<delivery> AddDelivery(DeliveryDto deliverydto)
    {
        var delivery = new delivery
        {
            deliveryid = deliverydto.deliveryid,
            idshipper = deliverydto.idshipper,
            thanhtoanid = deliverydto.thanhtoanid,
            pickuptime = deliverydto.pickuptime != default(DateTime) ? deliverydto.pickuptime : DateTime.UtcNow,
            deliverytime = deliverydto.deliverytime != default(DateTime) ? deliverydto.deliverytime : DateTime.UtcNow,
            deliverystatus = deliverydto.deliverystatus,
            idnguoiban = deliverydto.idnguoiban,
            idnguoimua = deliverydto.idnguoimua,
        };
        
        _context.delivery.Add(delivery);
        await _context.SaveChangesAsync();
        
        return delivery;

    }

    public async Task<List<delivery>> GetDeliveriesByNguoiban(int userId)
    {
        var delivery = await _context.delivery
            .Include(t => t.Thanhtoan)
            .Include(s => s.shipper)
            .Where(d => d.idnguoiban == userId)
            .ToListAsync();

        if (delivery == null)
        {
            Console.WriteLine("There is no delivery");
        }
        
       return delivery;
    }

    public async Task<List<delivery>> GetDeliveriesByShipperId(int userId)
    {
        var deliverryshipper = await _context.delivery
            .Include(t => t.Thanhtoan)
            .Include(s => s.shipper)
            .Where(d => d.idshipper == userId)
            .ToListAsync();
        
        return deliverryshipper;
    }

    public async Task<shipper> GetShipperByUserIdAsync(int userId)
    {
        var shipper = _context.shipper
            .Where(s => s.userid == userId)
            .FirstOrDefault();

        return shipper;
    }

    public async Task<List<shipper>> GetAllShipperAsync()
    {
        var shippers = await _context.shipper.ToListAsync();

        if (shippers == null)
        {
            Console.WriteLine("There is no shipper");
        }
        
        return shippers;
    }
    
    public async Task<bool> UpdateTrangThaiDonHangShipperAsync(int id, [FromBody] int trangthaidonhang)
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
}