using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NewsArticleService : INewsArticlesService
    {
        private readonly INewsArticleRepository iNewsArticleRepository;
        private static int counter = 0;

        
        public NewsArticleService()
        {
            iNewsArticleRepository = new NewsArticleRepository();
        }
        public void DeleteNewsArticle(NewsArticle newsArticle)
        {
            iNewsArticleRepository.DeleteNewsArticle(newsArticle);
        }

        public List<NewsArticle> getNewsArticles()
        {
            return iNewsArticleRepository.getNewsArticles();
        }

        public NewsArticle GetNewsArticlesById(string id)
        {
            return iNewsArticleRepository.GetNewsArticlesById(id);
        }

        public void SaveNewsArticle(NewsArticle newsArticle)
        {
            if (string.IsNullOrEmpty(newsArticle.NewsArticleId))
            {
                // Tìm kiếm giá trị NewsArticleId tiếp theo không trùng lặp
                int nextId = counter + 1;
                while (iNewsArticleRepository.GetNewsArticlesById(nextId.ToString()) != null)
                {
                    nextId++;
                }
                newsArticle.NewsArticleId = nextId.ToString();
            }
            iNewsArticleRepository.SaveNewsArticle(newsArticle);
            }

        public void UpdateNewsArticle(NewsArticle newsArticle)
        {
            iNewsArticleRepository.UpdateNewsArticle(newsArticle);
        }

        public List<NewsArticle> GetNewsArticlesByWriterId(int writerId)
        {
            return iNewsArticleRepository.GetNewsArticlesByWriterId(writerId);
        }

        public List<NewsArticle> GetReportStatistic(DateTime start, DateTime end)
        {
            return iNewsArticleRepository.GetReportStatistic(start, end);   
        }

        public List<NewsArticle> GetNewsArticlesByTitle(string title)
        {
            return iNewsArticleRepository.GetNewsArticlesByTitle(title);
        }
    }
}
