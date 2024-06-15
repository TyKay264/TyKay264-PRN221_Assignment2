using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listCategories = context.Categories.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCategories;
        }


        public static void SaveCategory(Category category)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Categories.Add(category);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCategory(Category category)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCategory(Category category)
        {
            try
            {
                /*using var context = new FunewsManagementDbContext();
                var p1 = context.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
                context.Categories.Remove(p1);
                context.SaveChanges();*/
                using var context = new FunewsManagementDbContext();

                var categoryToDelete = context.Categories
                    .Include(c => c.NewsArticles)
                    .SingleOrDefault(c => c.CategoryId == category.CategoryId);

                if (categoryToDelete == null)
                {
                    throw new Exception("Category not found.");
                }

                if (categoryToDelete.NewsArticles != null && categoryToDelete.NewsArticles.Any())
                {
                    throw new Exception("Cannot delete category because it is associated with one or more news articles.");
                }

                context.Categories.Remove(categoryToDelete);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Category GetCategoryById(short id)
        {
            using var context = new FunewsManagementDbContext();
            return context.Categories.SingleOrDefault(c => c.CategoryId.Equals(id));
        }

        public static List<Category> GetCategoryByName(string name)
        {
            using var context = new FunewsManagementDbContext();
            return context.Categories.Where(c => c.CategoryName.Contains(name)).ToList();
        }

    }
}
