using System.Threading.Tasks;
using Tasevski.Services.ShoppingCartAPI.Models.Dto;

namespace Tasevski.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponName);
    }
}
