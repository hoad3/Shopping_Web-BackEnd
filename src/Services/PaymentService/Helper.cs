using System.Security.Cryptography;
using System.Text;

namespace Web_2.Services.PaymentService;

public class Helper
{
    public static string Hash(string plaintext){
        HashAlgorithm algorithm = SHA512.Create();
        return Convert.ToHexString(algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext)));
        
    }
    // public static string HmacSha512(string plaintext, string key){
    //     HashAlgorithm algorithm = new HMACSHA512(Encoding.ASCII.GetBytes(key));
    //     return Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(plaintext)));
    // }
    
    public static string HmacSha512(string data, string key)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}