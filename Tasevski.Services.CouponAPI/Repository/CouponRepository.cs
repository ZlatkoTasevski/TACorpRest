
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tasevski.Services.CouponAPI.DbContexts;
using Tasevski.Services.CouponAPI.Models.Dto;

namespace Tasevski.Services.CouponAPI.Repository;
public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _db;
    private IMapper _mapper;

    public CouponRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<CouponDTO> GetCouponByCode(string couponCode)
    {
        var couponFromDb = await _db.Coupons.FirstOrDefaultAsync(u => u.CouponCode == couponCode);
        return _mapper.Map<CouponDTO>(couponFromDb);
    }
}
