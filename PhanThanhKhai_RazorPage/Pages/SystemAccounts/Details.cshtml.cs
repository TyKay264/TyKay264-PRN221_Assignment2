using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace PhanThanhKhai_RazorPage.Pages.SystemAccounts
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.FunewsManagementDbContext _context;

        public DetailsModel(BusinessObjects.FunewsManagementDbContext context)
        {
            _context = context;
        }

      public SystemAccount SystemAccount { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null || _context.SystemAccounts == null)
            {
                return NotFound();
            }

            var systemaccount = await _context.SystemAccounts.FirstOrDefaultAsync(m => m.AccountId == id);
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
    }
}
