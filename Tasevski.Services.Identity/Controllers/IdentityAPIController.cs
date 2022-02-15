using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.Identity.Dto;
using Tasevski.Services.Identity.Models;
using Tasevski.Services.Identity.Repository;
using Tasevski.Services.Identity.DbContexts;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Tasevski.Services.Identity.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class IdentityAPIController : Controller
    {
        protected ResponseDTO _response;
        private IUserRepo _userRepository;
        private readonly ApplicationDbContext _db;

        public IdentityAPIController(IUserRepo userRepository, ApplicationDbContext db)
        {
            _db = db;
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

        [HttpGet]
        [Route("{userId}")]
        public async Task<object> Get(string userId)
        {
            try
            {
                ApplicationUser users = await _userRepository.GetUserById(userId);
                _response.Result = users;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        //[HttpPost]
        //[Route("{Id}")]
        //public IActionResult LockUnlock([FromBody] string Id)
        //{
        //    var objFromDb = _db.Users.FirstOrDefault(u => u.Id == Id);
        //    if (objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Грешка при отклучи/заклучи" });
        //    }
        //    if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
        //    {
        //        objFromDb.LockoutEnd = DateTime.Now;
        //    }
        //    else
        //    {
        //        objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
        //    }
        //    _db.SaveChanges();
        //    return Json(new { success = true, message = "Операцијата е успешна!" });
        //}


        [HttpPost("LockUnlock")]
        public async Task<object> LockUnlock([FromBody] ApplicationUser user)
        {

            try
            {
                var isSuccess = await _userRepository.LockUnlock(user);
                _response.Result = isSuccess;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }
    }
}
