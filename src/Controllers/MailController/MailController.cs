using System.Text.Json.Serialization;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Web_2.AuthSetup;
using Web_2.Models;
using Web_2.Services.MailService;
using Web_2.Services.OTP;

namespace Web_2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MailController: ControllerBase
{
     private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly OtpService _otpService;

    public MailController(IEmailService emailService, OtpService otpService, IUserService userService)
    {
        _emailService = emailService;
        _otpService = otpService;
        _userService = userService;
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
    {
        if (string.IsNullOrWhiteSpace(emailRequest.RecipientEmail))
        {
            return BadRequest(new { success = false, message = "Recipient email is required." });

        }

        await _emailService.SendEmailAsync(emailRequest.RecipientEmail, emailRequest.Subject, emailRequest.Message);
        return Ok(new { success = true, message = "Email sent successfully!" });
    }
    
    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] EmailRequest emailRequest)
    {
        
        if (string.IsNullOrWhiteSpace(emailRequest.RecipientEmail))
        {
            return BadRequest("Recipient email is required.");
        }

        try
        {
            var otp = _otpService.GenerateOtp(emailRequest.RecipientEmail);
            await _emailService.SendOtpEmailAsync(emailRequest.RecipientEmail, otp);
            return Ok(new { success = true, message = "Email sent successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = "Recipient email is required." }); // Email chưa được đăng ký
        }
    }
    
    [HttpPost("verify-otp")]
    public IActionResult VerifyOtp([FromBody] OtpRequest otpRequest)
    {
        if (string.IsNullOrWhiteSpace(otpRequest.Email) || string.IsNullOrWhiteSpace(otpRequest.Otp))
        {
            return BadRequest(new { success = false, message = "Recipient email is required." });
        }

        var isValid = _otpService.ValidateOtp(otpRequest.Email, otpRequest.Otp);
            
            Console.WriteLine(otpRequest.Email, otpRequest.Otp);

        if (isValid)
        {
            return Ok(new { success = true, message = "OTP is valid." });
        }
        return BadRequest(new { success = false, message = "Recipient email is required." });
    }
}

public class OtpRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("otp")]
    public string Otp { get; set; }
}

public class EmailRequest
{
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}



