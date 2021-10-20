using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.Identity.Dto;
using Tasevski.Services.Identity.Models;
using Tasevski.Services.Identity.Repository;

namespace Tasevski.Services.Identity.Controllers
{
    [Route("api/users")]
    public class IdentityAPIController : Controller
    {
        protected ResponseDTO _response;
        private IUserRepo _userRepository;

        public IdentityAPIController(IUserRepo userRepository)
        {
            _userRepository = userRepository;
            this._response = new ResponseDTO();
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ApplicationUser> userDTOs = await _userRepository.GetUsers();
                _response.Result = userDTOs;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
