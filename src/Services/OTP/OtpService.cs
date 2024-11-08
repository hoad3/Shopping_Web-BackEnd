using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace Web_2.Services.OTP;

public class OtpService
{
    private readonly IMemoryCache _memoryCache;
    // private readonly ConcurrentDictionary<string, string> _otpStore = new();
    private readonly TimeSpan _otpStore = TimeSpan.FromMinutes(5);
    public OtpService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public string GenerateOtp(string email)
    {
        var otp = new Random().Next(100000, 999999).ToString();
        
        _memoryCache.Set(email, otp, _otpStore);

        // Log OTP đã được tạo
        Console.WriteLine($"Generated OTP: {otp} for Email: {email}");

        return otp;
    }

    public bool ValidateOtp(string email, string otp)
    {
        
        // Lấy giá trị OTP từ cache
        if (_memoryCache.TryGetValue(email, out string storedOtp))
        {
            // Log giá trị kiểm tra
            Console.WriteLine($"Validating OTP: {otp} for Email: {email}");

            if (storedOtp == otp)
            {
                // Nếu xác thực thành công, xóa OTP khỏi cache
                _memoryCache.Remove(email);
                return true;
            }
        }
        return false;
    }
}