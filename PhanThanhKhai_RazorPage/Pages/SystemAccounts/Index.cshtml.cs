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

namespace PhanThanhKhai_RazorPage.Pages.SystemAccounts
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;
        public IndexModel()
        {
            _systemAccountService = new SystemAccountService();
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                SystemAccount = await Task.Run(() => _systemAccountService.GetSystemAccountByName(SearchString));
            }
            else
            {
                SystemAccount = await Task.Run(() => _systemAccountService.GetSystemAccounts());
            }
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
    }
}
