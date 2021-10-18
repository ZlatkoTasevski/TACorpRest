
using AutoMapper;
using Tasevski.Services.CouponAPI.Models;
using Tasevski.Services.CouponAPI.Models.Dto;

namespace Tasevski.Services.CouponAPI;
public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDTO, Coupon>().ReverseMap();
        });
        return mappingConfig;
    }
}
