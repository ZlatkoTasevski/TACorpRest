using System.Net.Http;
using System.Threading.Tasks;
using Tasevski.Web.Models;
using Tasevski.Web.Services.IServices;

namespace Tasevski.Web.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateCategoryAsync<T>(CategoryDTO categoryDTO, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = categoryDTO,
                Url = SD.ProductAPIBase + "/api/categories/",
                AccessToken = token
            });
        }

        public async Task<T> DeleteCategoryAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/categories/" + id,
                AccessToken = token
            });
        }
        public async Task<T> GetAllCategoriesAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/categories/",
                AccessToken = token
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/categories/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDTO categoryDTO, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = categoryDTO,
                Url = SD.ProductAPIBase + "/api/categories/",
                AccessToken = token
            });
        }
    }
}
