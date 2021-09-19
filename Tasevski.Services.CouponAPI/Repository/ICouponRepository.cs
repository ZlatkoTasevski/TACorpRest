
using System.Threading.Tasks;
using Tasevski.Services.CouponAPI.Models.Dto;

namespace Tasevski.Services.CouponAPI.Repository;
public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCode(string couponCode);
}
