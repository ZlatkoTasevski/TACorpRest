using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.MessageBus;
using Tasevski.Services.ShoppingCartAPI.Messages;
using Tasevski.Services.ShoppingCartAPI.Models.Dto;
using Tasevski.Services.ShoppingCartAPI.Repository;
using Tasevski.Services.ShoppingCartAPI.Repository.IRepository;

namespace Tasevski.Services.ShoppingCartAPI.Controllers;

[ApiController]
[Route("api/cart")]
public class CartAPIController : Controller
{
    private readonly ICartRepository _cartRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly IMessageBus _messageBus;
    protected ResponseDTO _response;

    public CartAPIController(ICartRepository cartRepository, IMessageBus messageBus, ICouponRepository couponRepository)
    {
        _cartRepository = cartRepository;
        _couponRepository = couponRepository;
        _messageBus = messageBus;
        this._response = new ResponseDTO();
    }

    [HttpGet("GetCart/{userId}")]
    public async Task<object> GetCart(string userId)
    {
        try
        {
            CartDTO cartDTO = await _cartRepository.GetCartByUserId(userId);
            _response.Result = cartDTO;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }
    
    [HttpPost("AddCart")]
    public async Task<object> AddCart(CartDTO cartDto)
    {
        try
        {
            CartDTO cartDTO = await _cartRepository.CreateUpdateCart(cartDto);
            _response.Result = cartDTO;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpPost("UpdateCart")]
    public async Task<object> UpdateCart(CartDTO cartDto)
    {
        try
        {
            CartDTO cartDTO = await _cartRepository.CreateUpdateCart(cartDto);
            _response.Result = cartDTO;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpPost("RemoveCart")]
    public async Task<object> RemoveCart([FromBody]int cartId)
    {
        try
        {
           bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpPost("ApplyCoupon")]
    public async Task<object> ApplyCoupon([FromBody] CartDTO cartDTO)
    {
        try
        {
            bool isSuccess = await _cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpPost("RemoveCoupon")]
    public async Task<object> RemoveCoupon([FromBody] string userId)
    {

        try
        {
            bool isSuccess = await _cartRepository.RemoveCoupon(userId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpPost("Checkout")]
    public async Task<object> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
    {
        try
        {
            CartDTO cartDTO = await _cartRepository.GetCartByUserId(checkoutHeaderDTO.UserId);
            if(cartDTO == null)
            {
                return BadRequest();
            }
            if(!string.IsNullOrEmpty(checkoutHeaderDTO.CouponCode))
            {
                CouponDTO coupon = await _couponRepository.GetCoupon(checkoutHeaderDTO.CouponCode);
                if (checkoutHeaderDTO.DiscountTotal != coupon.DiscountAmount)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Вредноста на купонот е сменета, Ве молиме потврдете" };
                    _response.DisplayMessage = "Вредноста на купонот е сменета, Ве молиме потврдете";
                    return _response;
                }
            }


            checkoutHeaderDTO.CartDetails = cartDTO.CartDetails;
            //logic za dodavanje poraka vo procesot na naracka
            //await _messageBus.PublishMessage(checkoutHeaderDTO, "checkoutmessagetopic");
            await _messageBus.PublishMessage(checkoutHeaderDTO, "checkoutqueue");
            //otkoga kje se zavrsi processot so naplata ovdeka ja briseme kosnickata
            await _cartRepository.ClearCart(checkoutHeaderDTO.UserId);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }
}
