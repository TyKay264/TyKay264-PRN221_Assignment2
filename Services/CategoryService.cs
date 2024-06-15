using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository iCategoryRepository;
        private static short counter = 0;
        public CategoryService() {
            iCategoryRepository = new CategoryRepository();
        }
        public List<Category> GetCategories()
        {
            return iCategoryRepository.GetCategories();
        }

        public void SaveCategory(Category category)
        {
           /* short nextId = (short)(counter + 1);

            while (iCategoryRepository.GetCategoryById(nextId) != null)
            {
                nextId++;

                if (nextId == short.MaxValue)
                {
                    throw new InvalidOperationException("No available ID for SystemAccount");
                }
            } */
            iCategoryRepository.SaveCategory(category);
        }

        public Category GetCategoryById(short id)
        {
            return iCategoryRepository.GetCategoryById(id);
        }

        public void DeleteCategory(Category category)
        {
            iCategoryRepository.DeleteCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            iCategoryRepository.UpdateCategory(category);
        }

        public List<Category> GetCategoryByName(string name)
        {
            return iCategoryRepository.GetCategoryByName(name);
        }
    }
}
