using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasevski.Web.Models;
using Tasevski.Web.Services.IServices;

namespace Tasevski.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> UserIndex()
        {
            List<ApplicationUser> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _userService.GetAllUsersAsync<ResponseDTO>(accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ApplicationUser>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> LockUnlock(string userId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _userService.GetUserByIdAsync<ResponseDTO>(userId, accessToken);
            if (response != null && response.IsSuccess)
            {
                ApplicationUser model = JsonConvert.DeserializeObject<ApplicationUser>(Convert.ToString(response.Result));
                return View(model);
            }
            return View("~/AccessDenied.cshtml");
        }

        [HttpPost]
        [ActionName("LockUnlock")]
        //[Authorize(Roles = SD.Admin)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUnlock(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _userService.LockUnlock<ResponseDTO>(user, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(UserIndex));
                }
            }
            return View(user);
        }
    }
}
