using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.CouponAPI.Models.Dto;
using Tasevski.Services.CouponAPI.Repository;

namespace Tasevski.Services.CouponAPI.Controllers;
[ApiController]
[Route("api/coupon")]
public class CouponAPIController : Controller
{
    private readonly ICouponRepository _couponRepository;
    protected ResponseDTO _response;

    public CouponAPIController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
        this._response = new ResponseDTO();
    }

    [HttpGet("{code}")]
    public async Task<object> GetDiscountByCode(string code)
    {
        try
        {
            var coupon = await _couponRepository.GetCouponByCode(code);
            _response.Result = coupon;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }
}
