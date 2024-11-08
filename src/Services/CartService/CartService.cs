using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.Data;
using Web_2.Minio;
using Web_2.Models;
using Web_2.Models.Carts;

namespace Web_2.Services.CartService;

public class CartService: ICartService
{
    private readonly AppDbContext _context;
    private readonly IMinIOService _IOService;

    public CartService(AppDbContext context, IMinIOService IOService)
    {
        _context = context;
        _IOService = IOService;
    }

    public async Task<CartShoping> AddCartAsync([FromBody] CartShoping cartShoping)
    {
        // Kiểm tra xem UserId đã có CartId chưa
        var existingCart = await _context.CartShoping
            .FirstOrDefaultAsync(c => c.UserId == cartShoping.UserId);
        if (existingCart != null)
        {
            return null;
        }

        return existingCart;
    }

    public async Task<CartItemShoping> UpdateCartItemQuantityAsync(int CartId, int ProductId, [FromBody] CartItemShopingDto dto)
    {
        // Tìm sản phẩm trong giỏ hàng bằng cartItemId
        var cartItem = await _context.CartItemShoping
            .FirstOrDefaultAsync(ci => ci.CartId == CartId && ci.ProductId == ProductId);
        if (cartItem == null)
        {
            return null;
        }

        // Cập nhật số lượng
        cartItem.Quantity += dto.quantity;

        // Nếu quantity trở về 0 hoặc âm, có thể xóa sản phẩm
        if (cartItem.Quantity <= 0)
        {
            _context.CartItemShoping.Remove(cartItem);
        }
        else
        {
            _context.CartItemShoping.Update(cartItem);
        }

        await _context.SaveChangesAsync();

        // Trả về kết quả đã cập nhật
        return cartItem;
    }

    public async Task<CartItemShoping> AddCartItemAsync(int CartId, int ProductId)
    {
        var cart = await _context.CartShoping.FindAsync(CartId);
        if (cart == null)
        {
            return null;
        }

        // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
        var existingCartItem = await _context.CartItemShoping
            .FirstOrDefaultAsync(ci => ci.CartId == CartId && ci.ProductId == ProductId);
        if (existingCartItem != null)
        {
            // Nếu sản phẩm đã tồn tại, tăng số lượng
            existingCartItem.Quantity += 1;
            await _context.SaveChangesAsync();
            return existingCartItem;
        }
        // Nếu sản phẩm chưa tồn tại, tạo mới CartItemShoping với số lượng được chỉ định
        var newCartItem  = new CartItemShoping
        {
            CartId = CartId,
            ProductId = ProductId,
            Quantity = 1 // Set the initial quantity
        };
        _context.CartItemShoping.Add(newCartItem );
        await _context.SaveChangesAsync();

        // Trả về kết quả đã được tạo hoặc cập nhật
        return (newCartItem);
    }

    public async Task<bool> CheckCartItemAsync(int cartId, int productId)
    {
        var cartItem = await _context.CartItemShoping
            .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        return cartItem != null;
    }
    

    public async Task<CartShoping> GetCartsByIdAsync(int UserId)
    {
        var carts = await _context.CartShoping.FirstOrDefaultAsync(c => c.UserId == UserId);
    
        if (carts == null)
        {
            return null;
        }
    
        return carts;
    }

    public async Task<CartItemShoping> GetCartsByCartItemIDAsync(int ItemID)
    {
        var cartItemShoping = await _context.CartItemShoping
            .Include(c => c.CartShoping)  // Include CartShoping navigation property
            .Include(c => c.Product)      // Include Product navigation property
            .FirstOrDefaultAsync(c => c.CartItemId == ItemID);  // Use FirstOrDefaultAsync instead of FindAsync

    
        if (cartItemShoping == null)
        {
            return null;
        }
    
        return cartItemShoping;
    }

    public async Task<List<CartItemShoping>> GetItemShopingAsync(int cartId)
    {
        var getitemshoping = await _context.CartItemShoping
            .Include(c => c.CartShoping)
            .Include(c => c.Product)
            .Where(c => c.CartId == cartId)
            .ToListAsync();
        if (getitemshoping == null || !getitemshoping.Any())
        {
            return null;
        }

        foreach (var item in getitemshoping)
        {
            if (!string.IsNullOrEmpty(item.Product.Image))
            {
                item.Product.Image = await _IOService.GetFileUrl(item.Product.Image);
            }
        }

        return getitemshoping;
    }

    public async Task<CartItemShoping> DeleteCartAsync(int cartIteamId)
    {
        var findresult =
            await (from c in _context.CartItemShoping where c.CartItemId == cartIteamId select c).FirstOrDefaultAsync();
        if (findresult == null)
        {
            return null;
        }
        _context.CartItemShoping.Remove(findresult);
        await _context.SaveChangesAsync();
        return findresult;
    }
}