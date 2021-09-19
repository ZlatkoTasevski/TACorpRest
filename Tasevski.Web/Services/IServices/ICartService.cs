﻿
using System.Threading.Tasks;
using Tasevski.Web.Models;

namespace Tasevski.Web.Services.IServices;
public interface ICartService
{
    Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
    Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null);
    Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null);
    Task<T> RemoveFromCartAsync<T>(int cartId, string token = null);
    Task<T> ApplyCoupon<T>(CartDTO cartDTO, string token = null);
    Task<T> RemoveCoupon<T>(string userId, string token = null);
    Task<T> Checkout<T>(CartHeaderDTO cartHeaderDTO, string token = null);
}