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

namespace PhanThanhKhai_RazorPage.Pages.SystemAccounts
{
    public class DeleteModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public DeleteModel()
        {
            _systemAccountService = new SystemAccountService();
        }

        [BindProperty]
      public SystemAccount SystemAccount { get; set; } = default!;

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
                        if (Int16.Parse(role) != 3)
                        {
                            return RedirectToPage("/Privacy");
                        }
                    }
                    if (id == null || _systemAccountService.GetSystemAccounts() == null)
                    {
                        return NotFound();
                    }

                    var systemaccount = _systemAccountService.GetSystemAccountById(id);

                    if (systemaccount == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        SystemAccount = systemaccount;
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

            if (id == null || _systemAccountService.GetSystemAccounts() == null)
            {
                return NotFound();
            }
            var systemaccount = _systemAccountService.GetSystemAccountById(id);
            if (systemaccount != null)
            {
                SystemAccount = systemaccount;
                _systemAccountService.DeleteSystemAccount(SystemAccount);
            }

            return RedirectToPage("./Index");
        }
    }
}
