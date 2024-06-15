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

namespace PhanThanhKhai_RazorPage.Pages.SystemAccounts
{
    public class CreateModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;
        public SelectList AccountRoleOptions { get; set; }

        public CreateModel()
        {
            _systemAccountService = new SystemAccountService();
        }

        public IActionResult OnGet()
        {
            AccountRoleOptions = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Staff" },
            new SelectListItem { Value = "2", Text = "Lecture" }
        }, "Value", "Text");
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
                        if (Int16.Parse(role) != 3)
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

            }
            else { return RedirectToPage("/Login"); }
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _systemAccountService.GetSystemAccounts() == null || SystemAccount == null)
            {
                return Page();
            }
            _systemAccountService.SaveSystemAccount(SystemAccount);

            return RedirectToPage("./Index");
        }
    }
}
