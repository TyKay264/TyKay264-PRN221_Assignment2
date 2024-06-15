using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        void SaveCategory(Category category);
        Category GetCategoryById(short id);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        List<Category> GetCategoryByName(string name);
    }
}
