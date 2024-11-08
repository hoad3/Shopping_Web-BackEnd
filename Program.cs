using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Web_2.Controllers;
using Web_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Minio.AspNetCore;
using Nancy.Authentication.JwtBearer;
using Web_2.AuthSetup;
using Web_2.Data;
using Web_2.Minio;
using Web_2.Models.Delivery;
using Web_2.Models.Mail;
using Web_2.Models.VnPaymentRequest;
using Web_2.Services.AdminService;
using Web_2.Services.CartService;
using Web_2.Services.DeliveryService;
using Web_2.Services.Donmua;
using Web_2.Services.MailService;
using Web_2.Services.OTP;
using Web_2.Services.PaymentService;
using Web_2.Services.Product;
using Web_2.Services.Thanhtoan;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
//MinIO

        builder.Services.AddScoped<VnPaymentService>();

        builder.Services.Configure<VnPaymentRequest>(builder.Configuration.GetSection("Payment:VnPayment"));
        builder.Services.AddMinio(options =>
        {
            options.Endpoint = "localhost:9000";
            options.AccessKey = "y8tccwXv833XbwUf5vTr";
            options.SecretKey = "1DhEUwgqVhTzgOlYYqMosRhnmmptz4l2UkaR07JG";
        });
        builder.Services.AddScoped<IMinIOService,MinIOService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<TokenService>();
        builder.Services.AddScoped<IUserService,UserService>();
        builder.Services.AddScoped<IProductService,ProductService>();
        builder.Services.AddScoped<IInfoUserService, InfoUserService>();
        builder.Services.AddScoped<IThanhToanService, ThanhToanService>();
        builder.Services.AddScoped<IDonmuaService,DonmuaService>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IDeliveryService, DeliverySevice>();
        builder.Services.AddScoped<IEmailService,EmailService>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<OtpService>();
        
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMemoryCache();
        
       
        

// Add services to the container.
        builder.Services.AddControllers();
        
        
        // Configure database connection
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });            

        
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        
        
        
        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyMethod();
            options.WithOrigins("*");
        });
        app.MapControllers();
        app.Run();
        
    }
    
}
