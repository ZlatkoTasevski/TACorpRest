using System.Net.Http;
using System.Threading.Tasks;
using Tasevski.Web.Models;
using Tasevski.Web.Services.IServices;

namespace Tasevski.Web.Services
{
    public class UsersService : BaseService, IUserService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAllUsersAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.UserAPIBase + "/api/users/",
                AccessToken = token
            });
        }

        public async Task<T> GetUserByIdAsync<T>(string userId, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.UserAPIBase + "/api/users/" + userId,
                AccessToken = token
            });
        }

        //public async Task<T> LockUnlock<T>(string user, string token = null)
        //{
        //    return await this.SendAsync<T>(new ApiRequest()
        //    {
        //        ApiType = SD.ApiType.GET,
        //        Url = SD.UserAPIBase + "/api/users/"+user,
        //        AccessToken = token
        //    });
        //}


        public async Task<T> LockUnlock<T>(ApplicationUser user, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = user,
                Url = SD.UserAPIBase + "/api/users/LockUnlock",
                AccessToken = token
            });
        }
    }
}
