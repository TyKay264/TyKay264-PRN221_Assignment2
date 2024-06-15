using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using static PhanThanhKhai_RazorPage.Pages.IndexModel;
using System.Text.Json;
using Services;

namespace PhanThanhKhai_RazorPage.Pages.NewsArticles
{
    public class EditModel : PageModel
    {
        private readonly INewsArticlesService iNewsArticlesService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;

        public EditModel()
        {
            iNewsArticlesService = new NewsArticleService();
            iCategoryService = new CategoryService();
            iSystemAccountService = new SystemAccountService();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
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
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }
                        
                    }
                    if (id == null || iNewsArticlesService.getNewsArticles == null)
                    {
                        return NotFound();
                    }

                    var newsarticle = iNewsArticlesService.GetNewsArticlesById(id);
                    if (newsarticle == null)
                    {
                        return NotFound();
                    }
                    NewsArticle = newsarticle;
                    ViewData["CategoryId"] = new SelectList(iCategoryService.GetCategories(), "CategoryId", "CategoryDesciption");
                    ViewData["CreatedById"] = new SelectList(iSystemAccountService.GetSystemAccounts(), "AccountId", "AccountId");
                    iNewsArticlesService.UpdateNewsArticle(NewsArticle);
                    return Page();
                }
                catch (JsonException ex)
                {
                    return RedirectToPage("/Error");
                }

            }
            else { return RedirectToPage("/Login"); }
            
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewsArticle.ModifiedDate = DateTime.Now;
            iNewsArticlesService.UpdateNewsArticle(NewsArticle);
                    return RedirectToPage("./Index");
    }

        //private bool NewsArticleExists(string id)
        //{
        //  return (_context.NewsArticles?.Any(e => e.NewsArticleId == id)).GetValueOrDefault();
        //}
    }
}
