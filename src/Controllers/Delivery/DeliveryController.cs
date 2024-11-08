using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web_2.Models.Delivery;
using Web_2.Services.Thanhtoan;

namespace Web_2.Controllers.Delivery;

public class DeliveryController: ControllerBase
{
    private readonly IDeliveryService _deliveryService;
    private readonly IThanhToanService _thanhToanService;

    public DeliveryController(IDeliveryService deliveryService, IThanhToanService thanhToanService)
    {
        _deliveryService = deliveryService;
        _thanhToanService = thanhToanService;
    }

    [HttpPost]
    [Route("/AddDelivery")]
    public async Task<IActionResult> AddDelivery([FromBody] DeliveryDto deliveryDto)
    {
        var result = await _deliveryService.AddDelivery(deliveryDto);
        
        return Ok(result);
    }

    [HttpGet]
    [Route("/GetDeliveryByNguoiban/{nguoiban}")]
    public async Task<IActionResult> GetDeliveryByNguoiban(int nguoiban)
    {
        var results = await _deliveryService.GetDeliveriesByNguoiban(nguoiban);
        
        return Ok(results);
    }

    [HttpGet]
    [Route("/GetDeliverybyShipperID/{shipperId}")]
    public async Task<IActionResult> GetDeliveryByShipperID(int shipperId)
    {
        var results = await _deliveryService.GetDeliveriesByShipperId(shipperId);
        
        return Ok(results);
    }

    [HttpGet]
    [Route("/GetShipperByUserId/{userId}")]
    public async Task<IActionResult> GetShipperByUserId(int userId)
    {
        var results = await _deliveryService.GetShipperByUserIdAsync(userId);
        
        return Ok(results);
    }

    [HttpGet]
    [Route("/GetAllShipperAsync")]
    public async Task<IActionResult> GetShipperById()
    {
        var shipper = await _deliveryService.GetAllShipperAsync();
            
            return Ok(shipper);
    }
}