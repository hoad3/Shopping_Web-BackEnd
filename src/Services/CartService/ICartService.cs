using Microsoft.AspNetCore.Mvc;
using Web_2.Models.Carts;

namespace Web_2.Services.CartService;

public interface ICartService
{
    Task<CartShoping> AddCartAsync([FromBody] CartShoping cartShoping);
    Task<CartItemShoping> UpdateCartItemQuantityAsync(int CartId, int ProductId, [FromBody] CartItemShopingDto dto);
    Task<CartItemShoping> AddCartItemAsync(int cartId, int productId);
    Task<bool> CheckCartItemAsync(int cartId, int productId);
    Task<CartShoping> GetCartsByIdAsync(int UserId);
    Task<CartItemShoping> GetCartsByCartItemIDAsync(int ItemID);
    Task<List<CartItemShoping>> GetItemShopingAsync(int cartId);
    Task<CartItemShoping> DeleteCartAsync(int cartIteamId);
}