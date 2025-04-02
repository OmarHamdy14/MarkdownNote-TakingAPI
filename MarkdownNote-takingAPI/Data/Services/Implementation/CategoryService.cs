﻿using MarkdownNote_takingAPI.Models;

namespace MarkdownNote_takingAPI.Data.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IEntityBaseRepository<Category> _base;
        private readonly IMapper _mapper;
        public CategoryService(IEntityBaseRepository<Category> b, IMapper mapper)
        {
            _base = b;
            _mapper = mapper;
        }
        public async Task<Category> GetById(int categoryId)
        {
            return await _base.Get(n => n.Id == categoryId);
        }
        public async Task<List<Category>> GetAllCategoriesByUserId(string userId)
        {
            return await _base.GetAll(n => n.UserId == userId);
        }
        public async Task<Category> Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            category.CreatedAt = DateTime.UtcNow;
            await _base.Create(category);
            return category;
        }
        public async Task<Category> Update(Category category, UpdateCategoryDTO model)
        {
            _mapper.Map(category, model);
            await _base.Update(category);
            return category;
        }
        public async Task Delete(Category category)
        {
            await _base.Remove(category);
        }
    }
}
