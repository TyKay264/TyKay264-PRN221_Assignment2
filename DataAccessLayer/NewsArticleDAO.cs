using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class NewsArticleDAO
    {
        public static List<NewsArticle> getNewsArticles()
        {
            var listNewsArticles = new List<NewsArticle>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listNewsArticles =  context.NewsArticles
                        .Include(a => a.Category)
                        .Include(a => a.CreatedBy)
                        .ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNewsArticles;
        }

        public static void SaveNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.NewsArticles.Add(newsArticle);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<NewsArticle>(newsArticle).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                var p1 = context.NewsArticles.SingleOrDefault(c => c.NewsArticleId == newsArticle.NewsArticleId);
                context.NewsArticles.Remove(p1);
                context.SaveChanges();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static NewsArticle GetNewsArticlesById(string id)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles
                .Include(na => na.Category)
                .Include(na => na.CreatedBy)
                .SingleOrDefault(c => c.NewsArticleId.Equals(id));
        }

        public static List<NewsArticle> GetNewsArticlesByWriterId(int writerId)
        {
            var listNewsArticles = new List<NewsArticle>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listNewsArticles = context.NewsArticles.Where(c => c.CreatedById == writerId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNewsArticles;
        }

        public static List<NewsArticle> GetReportStatistic(DateTime start, DateTime end)
        {
            var listNewsArticles = new List<NewsArticle>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listNewsArticles = context.NewsArticles.Where(c => c.CreatedDate >= start && c.CreatedDate <= end).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNewsArticles;
        }

        public static List<NewsArticle> GetNewsArticlesByTitle(string title)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles.Where(c => c.NewsTitle.Contains(title)).ToList();
        }
    }
}
