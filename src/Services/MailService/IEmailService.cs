namespace Web_2.Services.MailService;

public interface IEmailService
{
    // Task SendEmailAsync(string toEmail, string subject, string body);
    // Task SendEmailAsync(string email, string subject, string message);;
    Task SendEmailAsync(string recipientEmail, string subject, string message);
    Task SendOtpEmailAsync(string recipientEmail, string otp);

}