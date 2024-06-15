using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public void DeleteNewsArticle(NewsArticle newsArticle)
        {
            NewsArticleDAO.DeleteNewsArticle(newsArticle);
        }

        public List<NewsArticle> getNewsArticles()
        {
            return NewsArticleDAO.getNewsArticles();
        }

        public NewsArticle GetNewsArticlesById(string id)
        {
            return NewsArticleDAO.GetNewsArticlesById(id);
        }

        public List<NewsArticle> GetNewsArticlesByWriterId(int writerId)
        {
            return NewsArticleDAO.GetNewsArticlesByWriterId(writerId);
        }

        public void SaveNewsArticle(NewsArticle newsArticle)
        {
            NewsArticleDAO.SaveNewsArticle(newsArticle);
        }

        public void UpdateNewsArticle(NewsArticle newsArticle)
        {
            NewsArticleDAO.UpdateNewsArticle(newsArticle);
        }

        public List<NewsArticle> GetReportStatistic(DateTime start, DateTime end)
        {
            return NewsArticleDAO.GetReportStatistic(start, end);
        }

        public List<NewsArticle> GetNewsArticlesByTitle(string title)
        {
            return NewsArticleDAO.GetNewsArticlesByTitle(title);
        }

    }
}
