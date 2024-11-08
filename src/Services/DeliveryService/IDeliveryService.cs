using Microsoft.AspNetCore.Mvc;

namespace Web_2.Models.Delivery;

public interface IDeliveryService
{
    Task<delivery> AddDelivery(DeliveryDto deliverydto);
    Task<List<delivery>> GetDeliveriesByNguoiban(int userId);
    Task<List<shipper>> GetAllShipperAsync();
    Task<List<delivery>> GetDeliveriesByShipperId(int userId);
    Task<shipper> GetShipperByUserIdAsync(int userId);
    Task<bool> UpdateTrangThaiDonHangShipperAsync(int id, [FromBody] int trangthaidonhang);
}