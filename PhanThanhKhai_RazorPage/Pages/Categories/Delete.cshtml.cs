using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using static PhanThanhKhai_RazorPage.Pages.IndexModel;
using System.Text.Json;

namespace PhanThanhKhai_RazorPage.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DeleteModel()
        {
            _categoryService = new CategoryService();
        }

        [BindProperty]
      public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
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
                        if (Int16.Parse(role) != 1)
                        {
                            return RedirectToPage("/Privacy");
                        }
                    }
                    if (id == null || _categoryService.GetCategories() == null)
                    {
                        return NotFound();
                    }

                    var category = _categoryService.GetCategoryById(id);

                    if (category == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        Category = category;
                    }
                    return Page();
                }
                catch (JsonException ex)
                {
                    return RedirectToPage("/Error");
                }

            }
            else { return RedirectToPage("/Login"); }
            
        }

        public async Task<IActionResult> OnPostAsync(short id)
        {
            if (id == null || _categoryService.GetCategories() == null)
            {
                return NotFound();
            }
            var category = _categoryService.GetCategoryById(id);

            if (category != null)
            {
                Category = category;
                _categoryService.DeleteCategory(Category);
            }

            return RedirectToPage("./Index");
        }
    }
}
