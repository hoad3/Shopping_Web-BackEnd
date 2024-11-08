    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Web_2.Data;
    using Web_2.Minio;
    using Web_2.Models;
    using Web_2.Models.Product;

    namespace Web_2.Services.Product;

    public class ProductService: IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMinIOService _minioService;

        public ProductService(AppDbContext context, IMinIOService minioService)
        {
            _context = context;
            _minioService = minioService;
        }

        public async Task<List<Models.Product.Product>> GetAllProductAsync(int page, int limit)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 4;

            var skip = (page - 1) * limit;
            var getAllProducts = await _context.product
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            foreach (var product in getAllProducts)
            {
                var imageUrl = await _minioService.GetFileUrl(product.Image);
                product.Image = imageUrl;
            }

            return getAllProducts;
        }

        public async Task<Models.Product.Product> GetProductByIdAsync(int id)
        {
            var findResult = await (from p in _context.product where p.id == id select p).FirstOrDefaultAsync();
            
            var url = await _minioService.GetFileUrl(findResult.Image);
            findResult.Image = url;
            return findResult;
        }
        public async Task<List<Models.Product.Product>> GetProductByUserIdAsync(int userId)
        {
            // Lấy tất cả sản phẩm của người bán theo userId
            var products = await _context.product
                .Where(p => p.Sellerid == userId)
                .ToListAsync();

            // Cập nhật URL hình ảnh cho từng sản phẩm
            foreach (var product in products)
            {
                product.Image = await _minioService.GetFileUrl(product.Image);
            }

            return products; // Trả về danh sách sản phẩm
        }

        public async Task<Models.Product.Product> AddProductAsync([FromForm] ProductCreateDto productDto)
        {
            // Tạo tên file duy nhất cho ảnh
            var objectName = Guid.NewGuid() + Path.GetExtension(productDto.image.FileName);
        
            // Mở stream để upload ảnh lên MinIO
            using (var stream = productDto.image.OpenReadStream())
            {
                // Upload ảnh lên MinIO và lấy URL của ảnh
                objectName = await _minioService.UploadFileAsync(objectName, stream, productDto.image.ContentType);
            }
            
        
            // Tạo đối tượng Product từ ProductDto và gán URL ảnh vào thuộc tính Image
            var product = new Models.Product.Product
            {
                Name = productDto.name,
                Value = productDto.value,
                Image = objectName, // Gán URL của ảnh từ MinIO vào đây
                Decription = productDto.decription,
                Stockquantity = productDto.stockquantity,
                Sellerid = productDto.sellerid,
                Daycreated = DateTime.UtcNow
            };
        
            // Thêm Product vào cơ sở dữ liệu
            _context.product.Add(product);
            await _context.SaveChangesAsync();
            var imageUrl = await _minioService.GetFileUrl(objectName);
            product.Image = imageUrl;
        
            return product;
        }

        public async Task<Models.Product.Product> ChangeProductAsync(int id, [FromForm] ProductCreateDto productdto)
        {
            var findAsync = await _context.product.FirstOrDefaultAsync(p => p.id == id);
            if (findAsync == null)
            {
                Console.WriteLine("Product not found" );
                return null;
            }
            // Kiểm tra nếu có file ảnh mới được upload
            if (productdto.image != null && productdto.image.Length > 0)
            {
                // Xóa ảnh cũ khỏi MinIO nếu có
                if (!string.IsNullOrEmpty(findAsync.Image))
                {
                    var deleteResult = await _minioService.DeleteFileAsync(findAsync.Image);
                    if (!deleteResult)
                    {
                        Console.WriteLine("Không thể xóa ảnh cũ trên MinIO.");
                        
                    }
                }

                // Tạo tên file mới cho ảnh và upload ảnh lên MinIO
                var newImageName = Guid.NewGuid() + Path.GetExtension(productdto.image.FileName);

                using (var stream = productdto.image.OpenReadStream())
                {
                    newImageName = await _minioService.UploadFileAsync(newImageName, stream, productdto.image.ContentType);
                }

                // Lấy URL của ảnh từ MinIO và cập nhật thuộc tính Image
                // var imageUrl = await _minioService.GetFileUrl(newImageName);
                // findAsync.Image = imageUrl; // Cập nhật URL mới của ảnh
                findAsync.Image = newImageName;
            }
            
            // update Product
            findAsync.Name = productdto.name;
            findAsync.Value = productdto.value;
            findAsync.Decription = productdto.decription;
            findAsync.Stockquantity = productdto.stockquantity;
            findAsync.Sellerid = productdto.sellerid;
            findAsync.Daycreated = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();

            return findAsync;

        }

        public async Task<Models.Product.Product> DeleteProductAsync(int id)
        {
            var findAsync = await _context.product.FirstOrDefaultAsync(p => p.id == id);
            // Tìm các CartItemShoping có ProductId tương ứng
            var cartItems = await _context.CartItemShoping
                .Where(ci => ci.ProductId == id)
                .ToListAsync();
            // Kiểm tra và xóa ảnh khỏi MinIO nếu có
            if (!string.IsNullOrEmpty(findAsync.Image))
            {
                var deleteresult = await _minioService.DeleteFileAsync(findAsync.Image);
                if (!deleteresult)
                {
                    // Nếu có lỗi khi xóa ảnh, trả về thông báo lỗi
                    Console.WriteLine("Không thể xóa ảnh từ MinIO");
                    return null;
                }
            }
            // Xóa các CartItemShoping
            _context.CartItemShoping.RemoveRange(cartItems);
            // Xóa Product
            _context.product.Remove(findAsync);
            await _context.SaveChangesAsync();
            return findAsync;
        }

        public async Task<List<Models.Product.Product>> SearchProductAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Models.Product.Product>();
            }

            var product = await _context.product
                .Where(p => p.Name.Contains(searchTerm.ToLower()) || p.Decription.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
            foreach (var results in product)
            {
                var imageUrl = await _minioService.GetFileUrl(results.Image);
                results.Image = imageUrl;
            }
            
            return product;
        }
        
    }