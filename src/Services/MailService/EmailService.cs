using System.Net.Mime;
using FirebaseAdmin.Auth;
using Web_2.Models.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Web_2.AuthSetup;
using Web_2.Controllers;

namespace Web_2.Services.MailService;

public class EmailService: IEmailService
{
    
    private readonly EmailSettings _emailSettings;
    
    private readonly IUserService _userService;

    public EmailService(IOptions<EmailSettings> emailSettings, IUserService userService)
    {
        _userService = userService;
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string message)
    {
        
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        emailMessage.To.Add(new MailboxAddress("", recipientEmail));
        emailMessage.Subject = subject;
        
        var bodyBuilder = new BodyBuilder { HtmlBody = message };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        
        try
        {
            await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);

            await client.SendAsync(emailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
    
    public async Task SendOtpEmailAsync(string recipientEmail, string otp)
    {
        bool isEmailRegister = await _userService.IsEmailRegisteredAsync(recipientEmail);
        
        if (!isEmailRegister)
        {
            throw new Exception("Email chưa được đăng ký trong hệ thống.");
        }
        var subject = "Your OTP Code";
        var message = $"Your OTP code is: {otp}";

        await SendEmailAsync(recipientEmail, subject, message);
    }
   
    
}