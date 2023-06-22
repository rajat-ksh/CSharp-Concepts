using CatalogService.Models;

namespace CatalogService.Service.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public void AddCategory(Category category);
        public void DeleteCategory(Category category);
        public void UpdateCategory(int id);
        public Category GetCategoryById(int id);
    }
}
