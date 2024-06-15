using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using static PhanThanhKhai_RazorPage.Pages.IndexModel;
using System.Text.Json;

namespace PhanThanhKhai_RazorPage.Pages.NewsArticles
{
    public class DeleteModel : PageModel
    {
        private readonly INewsArticlesService iNewsArticlesService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;

        public DeleteModel()
        {
            iNewsArticlesService = new NewsArticleService();
            iCategoryService = new CategoryService();
            iSystemAccountService = new SystemAccountService();
        }

        [BindProperty]
      public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || iNewsArticlesService.getNewsArticles() == null)
            {
                return NotFound();
            }

            var newsarticle = iNewsArticlesService.GetNewsArticlesById(id);

            if (newsarticle == null)
            {
                return NotFound();
            }
            else 
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (Request.Cookies.TryGetValue("UserInfo", out string cookieValue))
            {
                try
                {
                    var userInfo = JsonSerializer.Deserialize<UserInfoModel>(cookieValue);
                    if (userInfo != null)
                    {
                        string username = userInfo.Username;
                        string role = userInfo.Role;
                        string idUser = userInfo.Id;
                        ViewData["Id"] = idUser;
                        if (Int16.Parse(role) != 1)
                        {
                            return RedirectToPage("/Privacy");
                        }
                    }
                    if (id == null || iNewsArticlesService.getNewsArticles() == null)
                    {
                        return NotFound();
                    }
                    var newsarticle = iNewsArticlesService.GetNewsArticlesById(id);

                    if (newsarticle != null)
                    {
                        NewsArticle = newsarticle;
                        iNewsArticlesService.DeleteNewsArticle(NewsArticle);
                    }

                    return RedirectToPage("./Index");
                }
                catch (JsonException ex)
                {
                    return RedirectToPage("/Error");
                }

            }
            else { return RedirectToPage("/Login"); }
            
        }
    }
}
