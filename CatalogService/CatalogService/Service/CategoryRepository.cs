using CatalogService.Models;
using CatalogService.Service.Interfaces;

namespace CatalogService.Service
{
    public class CategoryRepository : ICategoryRepository
    {
        private List<Category> _categories;

        CategoryRepository()
        {
            _categories = new List<Category>();
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _categories.Remove(category);
        }

        public void DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
