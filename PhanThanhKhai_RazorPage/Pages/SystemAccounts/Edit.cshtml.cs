using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using static PhanThanhKhai_RazorPage.Pages.IndexModel;
using System.Text.Json;

namespace PhanThanhKhai_RazorPage.Pages.SystemAccounts
{
    public class EditModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;
        public SelectList AccountRoleOptions { get; set; }
        public EditModel()
        {
            _systemAccountService = new SystemAccountService();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
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
                    SystemAccount = systemaccount;
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
            _systemAccountService.UpdateSystemAccount(SystemAccount);
            return RedirectToPage("./Index");
        }

        //private bool SystemAccountExists(short id)
        //{
        //  return (_context.SystemAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        //}
    }
}
