
using System.Threading.Tasks;
using Tasevski.Web.Models;

namespace Tasevski.Web.Services.IServices;
public interface ICouponService
{
    Task<T> GetCouponAsync<T>(string couponCode, string token = null);   
}
