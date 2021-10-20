
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tasevski.Web.Models;
using Tasevski.Web.Services.IServices;

namespace Tasevski.Web.Services;
public class CartService : BaseService, ICartService //BaseService sakame da prakjame async Calls
{
    private readonly IHttpClientFactory _clientFactory;

    public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
            AccessToken = token
        });
    }

    public async Task<T> ApplyCoupon<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
            AccessToken = token
        });
    }

    public async Task<T> RemoveCoupon<T>(string userId, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = userId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon",
            AccessToken = token
        });
    }

    public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId,
            AccessToken = token
        });
    }


    public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
            AccessToken = token
        });
    }

    public async Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
            AccessToken = token
        });
    }

    public async Task<T> Checkout<T>(CartHeaderDTO cartHeaderDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartHeaderDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/checkout",
            AccessToken = token
        });
    }

}
