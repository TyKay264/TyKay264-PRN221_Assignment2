using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Services;
using static PhanThanhKhai_RazorPage.Pages.IndexModel;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace PhanThanhKhai_RazorPage.Pages.NewsArticles
{
    public class CreateModel : PageModel
    {
        private readonly INewsArticlesService iNewsArticlesService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;

        public CreateModel()
        {
            iNewsArticlesService = new NewsArticleService();
			iCategoryService = new CategoryService();
			iSystemAccountService = new SystemAccountService();
		}

        public IActionResult OnGet()
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
                        string id = userInfo.Id;
                        ViewData["Id"] = id;
                        ViewData["CategoryId"] = new SelectList(iCategoryService.GetCategories(), "CategoryId", "CategoryDesciption");
                        ViewData["CreatedById"] = new SelectList(
                            new List<SelectListItem> { new SelectListItem { Value = userInfo.Id, Text = userInfo.Username } },
                            "Value", "Text");
                        if (Int16.Parse(role) != 1)
                        {
                            return RedirectToPage("/Privacy");
                        }
                    }
                    return Page();
                }
                catch (JsonException ex)
                {
                    return RedirectToPage("/Error");
                }
                
            } else { return RedirectToPage("/Login"); }
        }

                [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            NewsArticle.ModifiedDate = DateTime.Now;
            NewsArticle.NewsStatus = true;
            iNewsArticlesService.SaveNewsArticle(NewsArticle);
            return RedirectToPage("./Index");
        }
    }
}
