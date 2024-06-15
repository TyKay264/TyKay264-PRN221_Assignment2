using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void DeleteCategory(Category category)
        {
            CategoryDAO.DeleteCategory(category);
        }

        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }

        public Category GetCategoryById(short id)
        {
            return CategoryDAO.GetCategoryById(id);
        }

        public void SaveCategory(Category category)
        {
            CategoryDAO.SaveCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            CategoryDAO.UpdateCategory(category);
        }

        public List<Category> GetCategoryByName(string name)
        {
            return CategoryDAO.GetCategoryByName(name);
        }
    }
}
