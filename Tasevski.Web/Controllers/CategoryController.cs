using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Web.Models;
using Tasevski.Web.Services.IServices;

namespace Tasevski.Web.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryIndex()
        {
            List<CategoryDTO> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _categoryService.GetAllCategoriesAsync<ResponseDTO>(accessToken);
            if (response != null && response.IsSuccess)
            {                
                list = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _categoryService.CreateCategoryAsync<ResponseDTO>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }
            return View(model);
        }

        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> CategoryEdit(int categoryId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _categoryService.GetCategoryByIdAsync<ResponseDTO>(categoryId, accessToken);
            if (response != null && response.IsSuccess)
            {
                CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return View("~/AccessDenied.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = SD.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _categoryService.UpdateCategoryAsync<ResponseDTO>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }
            return View(model);
        }

        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> CategoryDelete(int categoryId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _categoryService.GetCategoryByIdAsync<ResponseDTO>(categoryId, accessToken);
            if (response != null && response.IsSuccess)
            {
                CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = SD.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _categoryService.DeleteCategoryAsync<ResponseDTO>(model.CategoryId, accessToken);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }
            return View(model);
        }
    }

}