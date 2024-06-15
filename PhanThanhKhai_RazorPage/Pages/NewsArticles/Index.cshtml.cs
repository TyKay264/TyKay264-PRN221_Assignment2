using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Services;

namespace PhanThanhKhai_RazorPage.Pages.NewsArticles
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticlesService iNewsArticlesService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;

        public IndexModel()
        {
            iNewsArticlesService = new NewsArticleService();
            iCategoryService = new CategoryService();
            iSystemAccountService = new SystemAccountService();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<NewsArticle> NewsArticle { get; set; } = new List<NewsArticle>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                NewsArticle = await Task.Run(() => iNewsArticlesService.GetNewsArticlesByTitle(SearchString));
            }
            else
            {
                NewsArticle = await Task.Run(() => iNewsArticlesService.getNewsArticles());
            }

            return Page();
        }
    }
}
