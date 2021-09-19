using System;
using System.Threading.Tasks;
using Tasevski.Web.Models;

namespace Tasevski.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        ResponseDTO responseModel { get; set; }

        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}