namespace MarkdownNote_takingAPI.Data.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetById(int categoryId);
        Task<List<Category>> GetAllCategoriesByUserId(string userId);
        Task<Category> Create(CreateCategoryDTO model);
        Task<Category> Update(Category category, UpdateCategoryDTO model);
        Task Delete(Category category);
    }
}
