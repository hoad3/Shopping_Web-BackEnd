using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Web_2.Models.VnPaymentRequest;
using Web_2.Services.Donmua;
using Web_2.Services.PaymentService;

namespace Web_2.Controllers.PaymentController;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly VnPaymentService _vnPaymentService;
    private readonly IDonmuaService _donmuaService;

    public PaymentController(IOptions<VnPaymentRequest> options, IDonmuaService donmuaService)
    {
        _donmuaService=donmuaService;
        _vnPaymentService = new VnPaymentService(options);
    }

    [HttpPost("CreatePayment")]
    public IActionResult CreatePayment([FromBody] Invoice invoice)
    {
        if (invoice.Amount <= 0)
        {
            return BadRequest("Số tiền không hợp lệ.");
        }

        
        // Tạo URL thanh toán từ đối tượng Invoice và gửi yêu cầu lên VNPay
        string paymentUrl = _vnPaymentService.ToUrl(invoice, HttpContext);

        // Trả về URL để chuyển hướng người dùng đến trang thanh toán của VNPay
        return Ok(new { paymentUrl });
    }

    [HttpGet("PaymentResult")]
    public async Task<IActionResult> PaymentResult([FromQuery] VnPayment obj)
    {
        return Ok(obj);
    }
    
    private bool ValidateSignature(Dictionary<string, string> queryParams, string hashSecret)
    {
        // Lấy vnp_SecureHash và loại bỏ nó khỏi queryParams để tính toán chữ ký
        var vnp_SecureHash = queryParams["vnp_SecureHash"];
        queryParams.Remove("vnp_SecureHash");

        // Sắp xếp các tham số theo thứ tự chữ cái
        var sortedParams = new SortedDictionary<string, string>(queryParams);

        // Tạo chuỗi rawData không bao gồm hashSecret
        string rawData = string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
    
        // Tính toán chữ ký HMAC SHA512
        string computedHash = Helper.HmacSha512(rawData, hashSecret);

        // So sánh chữ ký đã tính với vnp_SecureHash (không phân biệt chữ hoa/thường)
        return computedHash.Equals(vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
    }
}