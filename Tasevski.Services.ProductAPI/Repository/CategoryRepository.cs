using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasevski.Services.ProductAPI.DbContexts;
using Tasevski.Services.ProductAPI.Models;
using Tasevski.Services.ProductAPI.Models.Dto;

namespace Tasevski.Services.ProductAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> CreateUpdateCategory(CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            if (category.CategoryId > 0)
            {
                _db.Categories.Update(category);
            }
            else
            {
                _db.Categories.Add(category);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                Category category = await _db.Categories.FirstOrDefaultAsync(u => u.CategoryId == categoryId);
                if (category == null)
                {
                    return false;
                }
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            IEnumerable<Category> categoryList = await _db.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoryList);
        }

        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            Category category = await _db.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
