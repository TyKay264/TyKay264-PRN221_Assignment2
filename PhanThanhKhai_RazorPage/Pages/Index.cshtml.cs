using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text.Json;

namespace PhanThanhKhai_RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies.TryGetValue("UserInfo", out string cookieValue))
            {
                try
                {
                    var userInfo = JsonSerializer.Deserialize<UserInfoModel>(cookieValue);
                    var username = userInfo.Username;
                    var role = userInfo.Role;
                    var id = userInfo.Id;
                    ViewData["Username"] = username;
                    ViewData["Role"] = role;
                    ViewData["Id"] = id;
                    return Page();
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error deserializing JSON from cookie 'UserInfo': {ex.Message}");
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                Console.WriteLine("Cookie 'UserInfo' not found.");
                var username = "Guest";
                var role = "None";
                var id = "0";
                ViewData["Username"] = username;
                ViewData["Role"] = role;
                ViewData["Id"] = id;
                return Page();
                //return RedirectToPage("/Login");
            }
        }

        //public IActionResult OnGet()
        //{
        //    if (Request.Cookies.TryGetValue("UserInfo", out string cookieValue))
        //    {
        //        try
        //        {
        //            var userInfo = JsonSerializer.Deserialize<UserInfoModel>(cookieValue);
        //            var username = userInfo.Username;
        //            var role = userInfo.Role;
        //            var id = userInfo.Id;
        //        }
        //        catch (JsonException ex)
        //        {
        //            _logger.LogError($"Error deserializing JSON from cookie 'UserInfo': {ex.Message}");
        //            var username = "Guest";
        //            var role = "None";
        //            var id = "0";
        //            ViewData["Username"] = username;
        //            ViewData["Role"] = role;
        //            ViewData["Id"] = id;
        //        }
        //    }
        //    else
        //    {
        //        // No cookie found, set default values
        //        var username = "Guest";
        //        var role = "None";
        //        var id = "0";
        //        ViewData["Username"] = username;
        //        ViewData["Role"] = role;
        //        ViewData["Id"] = id;
        //    }
        //    return Page();
        //}



        public class UserInfoModel
		{
			public string Username { get; set; }
			public string Role { get; set; }
            public string Id { get; set; }
        }
	}
}
