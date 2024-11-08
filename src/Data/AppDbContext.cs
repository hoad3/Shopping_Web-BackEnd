using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_2.Models;
using Web_2.Models.Carts;
using Web_2.Models.Delivery;
using Web_2.Models.Product;
using Web_2.Models.Thanhtoan;
using Web_2.Models.VnPaymentRequest;

namespace Web_2.Data;
public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> USER  { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<CartShoping> CartShoping { get; set; }
        public DbSet<CartItemShoping> CartItemShoping { get; set; }
        public DbSet<InformationUser> InformationUser { get; set; }
        public DbSet<ThanhToan> ThanhToan { get; set; }
        public DbSet<Donmua> Donmua { get; set; }
        public DbSet<shipper> shipper{get;set;}
        public DbSet<delivery> delivery { get; set; }
        public DbSet<Invoice> invoice { get; set; }
        public DbSet<InvoiceDetail> invoiceDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Đảm bảo rằng EF biết các mối quan hệ và khóa chính

            modelBuilder.Entity<Invoice>()
                .ToTable("invoice")
                .HasKey(i => i.InvoiceId);

            modelBuilder.Entity<InvoiceDetail>()
                .HasKey(id => new { id.InvoiceId, id.ProductId });
            // Thiết lập quan hệ giữa Invoice và InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne<Invoice>()
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(id => id.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            // Thiết lập quan hệ giữa Product và InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne<Product>()
                .WithMany()  // Nếu Product không có collection InvoiceDetails
                .HasForeignKey(id => id.ProductId)
                .OnDelete(DeleteBehavior.Restrict); 
            

            modelBuilder.Entity<shipper>()
                .ToTable("shipper")
                .HasKey(s => s.idshipper);
            
            
            modelBuilder.Entity<shipper>()
                .Property(s => s.idshipper)
                .ValueGeneratedOnAdd(); 
            
            modelBuilder.Entity<delivery>()
                .ToTable("delivery")
                .HasKey(d => d.deliveryid);
            
            modelBuilder.Entity<delivery>()
                .Property(d => d.deliveryid)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<delivery>()
                .HasOne(d => d.shipper)
                .WithMany()
                .HasForeignKey(d => d.idshipper)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<delivery>()
                .HasOne(d => d.Thanhtoan)
                .WithMany()
                .HasForeignKey(d => d.thanhtoanid)
                .OnDelete(DeleteBehavior.Restrict);  
            
            modelBuilder.Entity<Donmua>()
                .ToTable("Donmua") // Xác định tên bảng là "ThanhToan"
                    .HasKey(t => t.Iddonmua);
            
            modelBuilder.Entity<Donmua>()
                .Property(t => t.Iddonmua)
                .ValueGeneratedOnAdd(); // Cấu hình tự động tăng giá trị cho cột Id
            
            // Thiết lập khóa ngoại cho bảng Donmua
            
            modelBuilder.Entity<Donmua>()
                .HasOne<User>()  // Mối quan hệ với bảng User cho idnguoimua
                .WithMany()
                .HasForeignKey(d => d.idnguoimua)
                .OnDelete(DeleteBehavior.Restrict);  // Tùy chọn hành vi xóa khi người dùng bị xóa
    
            modelBuilder.Entity<Donmua>()
                .HasOne<User>()  // Mối quan hệ với bảng User cho idnguoiban
                .WithMany()
                .HasForeignKey(d => d.idnguoiban)
                .OnDelete(DeleteBehavior.Restrict);  // Tùy chọn hành vi xóa khi người bán bị xóa
    
            modelBuilder.Entity<Donmua>()
                .HasOne(d => d.Product)  // Mối quan hệ với bảng Product cho idproduct
                .WithMany()
                .HasForeignKey(d => d.idproduct)
                .OnDelete(DeleteBehavior.Restrict);  // Tùy chọn hành vi xóa khi sản phẩm bị xóa
            
            // Thiết lập khóa chính cho bảng Thanhtoan
            modelBuilder.Entity<ThanhToan>()
                .ToTable("ThanhToan") // Xác định tên bảng là "ThanhToan"
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<ThanhToan>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd(); // Cấu hình tự động tăng giá trị cho cột Id
            
            // Thiết lập khóa ngoại Idnguoimua liên kết với bảng User
            modelBuilder.Entity<ThanhToan>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.Idnguoimua)
                .OnDelete(DeleteBehavior.Restrict);  // Tùy chọn hành vi xóa

            // Thiết lập khóa ngoại Idnguoiban liên kết với bảng User
            modelBuilder.Entity<ThanhToan>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.Idnguoiban)
                .OnDelete(DeleteBehavior.Restrict);

            // Thiết lập khóa ngoại ProductId liên kết với bảng Product
            modelBuilder.Entity<ThanhToan>()
                .HasOne(t => t.Product)
                .WithMany()
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Thiết lập khóa chính cho bảng CartShoping
            modelBuilder.Entity<CartShoping>()
                .HasKey(c => c.CartId);
            // Cấu hình thuộc tính CreatedAt
            
            modelBuilder.Entity<CartShoping>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            // Cấu hình CartId là auto-increment
            
            modelBuilder.Entity<CartShoping>()
                .Property(c => c.CartId)
                .ValueGeneratedOnAdd();
            
            // Cấu hình khóa chính cho thực thể CartItemShoping
            modelBuilder.Entity<CartItemShoping>()
                .HasKey(c => c.CartItemId);
            // modelBuilder.Entity<CartItemShoping>().HasNoKey(); // Đánh dấu CartItemShoping là Keyless Entity Type
            
            modelBuilder.Entity<CartItemShoping>()
                .Property(c => c.CartItemId)
                .ValueGeneratedOnAdd();
            
            // Định nghĩa mối quan hệ một-nhiều giữa CartShoping và CartItemShoping
            modelBuilder.Entity<CartShoping>()
                .HasMany(c => c.CartItem)
                .WithOne(c => c.CartShoping)
                .HasForeignKey(c => c.CartId);
            
            //cấu hình khóa chính cho InformationUser
            modelBuilder.Entity<InformationUser>()
                .HasKey(i => i.Idname);
            
            // cấu hình mối quan hệ 1-1 cho InformationUser và User
            modelBuilder.Entity<User>()
                .HasOne<InformationUser>(u => u.InformationUser)
                .WithOne(i => i.User)
                .HasForeignKey<InformationUser>(i => i.User_id);
            
            modelBuilder.HasDefaultSchema("Data");
        }
    }
