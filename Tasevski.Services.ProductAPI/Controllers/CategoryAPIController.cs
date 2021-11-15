using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.ProductAPI.Models;
using Tasevski.Services.ProductAPI.Models.Dto;
using Tasevski.Services.ProductAPI.Repository;

namespace Tasevski.Services.ProductAPI.Controllers
{
    [Route("api/categories")]
    public class CategoryAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private ICategoryRepository _categoryRepository;

        public CategoryAPIController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            this._response = new ResponseDTO();
        }
        
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<CategoryDTO> productDTOs = await _categoryRepository.GetCategories();
                _response.Result = productDTOs;
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
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                CategoryDTO categoryDTOs = await _categoryRepository.GetCategoryById(id);
                _response.Result = categoryDTOs;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<object> Post([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                CategoryDTO model = await _categoryRepository.CreateUpdateCategory(categoryDTO);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<object> Put([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                CategoryDTO model = await _categoryRepository.CreateUpdateCategory(categoryDTO);
                _response.Result = model;
            }
            catch (Exception ex)
            {      
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _categoryRepository.DeleteCategory(id);
                _response.Result = isSuccess;
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