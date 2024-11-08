using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_2.Models;
using Web_2.Models.Carts;
using Web_2.Services.CartService;

namespace Web_2.Controllers;


[ApiController]
public class CartsController: ControllerBase
{
    // private readonly AppDbContext _context;
    // private readonly IMinIOService _minioService;
    private readonly ICartService _cartService;
    public CartsController(ICartService cartService)
    {
        // _context = context;
        // _minioService = minioService;
        _cartService = cartService;
    }

    [HttpPost("AddCart")]
    public async Task<ActionResult<CartShoping>> AddCart([FromBody] CartShoping cartShoping)
    {
        // Kiểm tra xem UserId đã có CartId chưa
        var existingCart = await _cartService.AddCartAsync(cartShoping);
    
        if (existingCart != null)
        {
            return BadRequest("A cart for this user already exists.");
        }

        // _context.CartShoping.Add(cartShoping);
        // await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCartsById), new { cartsId = cartShoping.CartId }, cartShoping);
    }
    [HttpPost("AddCartItem/{CartId}/{ProductId}")]
    public async Task<ActionResult<CartItemShoping>> AddCartItem(int CartId, int ProductId)
    {
        // Tìm giỏ hàng bằng CartId từ URL
        var cartresult = await _cartService.AddCartItemAsync(CartId, ProductId);
        
        return Ok(cartresult);
    }
    
    [HttpPatch("UpdateCartItemQuantity/{CartId}/{ProductId}")]
    public async Task<ActionResult<CartItemShoping>> UpdateCartItemQuantity(int CartId, int ProductId, [FromBody] CartItemShopingDto dto)
    {
        // Tìm sản phẩm trong giỏ hàng bằng cartItemId
        var cartItem = await _cartService.UpdateCartItemQuantityAsync(CartId, ProductId, dto);
        if (cartItem == null)
        {
            return NotFound(new { Message = "Cart item not found" });
        }

        // Trả về kết quả đã cập nhật
        return Ok(cartItem);
    }
    [HttpGet("CheckCartItem/{cartId}/{productId}")]
    public async Task<ActionResult<bool>> CheckCartItem(int cartId, int productId)
    {
        var cartItem = await _cartService.CheckCartItemAsync(cartId, productId);
        return Ok(cartItem != null);
    }
    
    [HttpGet("CartID/{UserId}")]
    public async Task<ActionResult<CartShoping>> GetCartsById(int UserId)
    {
        var carts = await _cartService.GetCartsByIdAsync(UserId);
    
        if (carts == null)
        {
            return NotFound();
        }
    
        return carts;
    }
    [HttpGet("ItemShoping/{ItemID}")]
    public async Task<ActionResult<CartItemShoping>> GetCartsByCartItemID(int ItemID)
    {
        var cartItemShoping = await _cartService.GetCartsByCartItemIDAsync(ItemID);  

    
        if (cartItemShoping == null)
        {
            return NotFound();
        }
    
        return cartItemShoping;
    }
    

    [HttpGet("Get_Item_Shoping/{CartId}")]
    public async Task<ActionResult<CartItemShoping>> GetItemShoping(int CartId)
    {
        var getitemshoping = await _cartService.GetItemShopingAsync(CartId);
        if (getitemshoping == null || !getitemshoping.Any())
        {
            return NotFound();
        }

        return Ok(getitemshoping);
    }

    [HttpDelete("Delete_Cart/{cartIteamId}")]
    public async Task<ActionResult<CartItemShoping>> DeleteCart(int cartIteamId)
    {
        var findresult = await _cartService.DeleteCartAsync(cartIteamId);
        if (findresult == null)
        {
            return NotFound("khong co hang hoa");
        }
        // _context.CartItemShoping.Remove(findresult);
        // await _context.SaveChangesAsync();
        return Ok(new { Message = "User deleted successfully" });
    }
    
}