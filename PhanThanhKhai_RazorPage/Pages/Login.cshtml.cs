using BusinessObjects;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;
using System.Configuration;

namespace PhanThanhKhai_RazorPage.Pages
{
	public class LoginModel : PageModel
	{
		private readonly ISystemAccountService _systemAccountService;
        private readonly IConfiguration configuration;
        private readonly string predefinedEmail;
        private readonly string predefinedPassword;
        public LoginModel()
		{
			_systemAccountService = new SystemAccountService();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            predefinedEmail = configuration["SystemAccount:Admin:AccountEmail"];
            predefinedPassword = configuration["SystemAccount:Admin:AccountPassword"];
        }
		[BindProperty]
		public Credential Credentials { get; set; }
		public class Credential
		{
			[Required]
			public string Username { get; set; }
			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
                string enteredEmail = Credentials.Username;
                string enteredPassword = Credentials.Password;

                if (enteredEmail.Equals(predefinedEmail, StringComparison.OrdinalIgnoreCase) &&
                    enteredPassword.Equals(predefinedPassword))
                {
                    var adminClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, predefinedEmail),
                        new Claim("FullName", "Admin"),
                        new Claim(ClaimTypes.Role, "3"),
                        new Claim("Id", "0")
                    };

                    var adminClaimsIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(adminClaimsIdentity));

                    HttpContext.Session.SetString("Username", predefinedEmail);
                    HttpContext.Session.SetString("FullName", "Admin");

                    var adminInfo = new
                    {
                        Username = predefinedEmail,
                        Role = "3",
                        Id = "0"
                    };

                    var adminInfoJson = JsonSerializer.Serialize(adminInfo);

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(7),
                        IsEssential = true,
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };

                    HttpContext.Response.Cookies.Append("UserInfo", adminInfoJson, cookieOptions);

                    return RedirectToPage("/Index");
                }

                var user = _systemAccountService.GetSystemAccountByUsername(Credentials.Username);
				if (user != null && user.AccountPassword == Credentials.Password) 
				{
					var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.AccountEmail),
					new Claim("FullName", user.AccountName),
					new Claim(ClaimTypes.Role, user.AccountRole.ToString()),
					new Claim("Id", user.AccountId.ToString())
				};

					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

					HttpContext.Session.SetString("Username", user.AccountEmail);
					HttpContext.Session.SetString("FullName", user.AccountName);

					var userInfo = new
					{
						Username = user.AccountEmail,
						Role = user.AccountRole.ToString(),
						Id = user.AccountId.ToString(),
					};

					var userInfoJson = JsonSerializer.Serialize(userInfo);

					// Set the cookie options
					var cookieOptions = new CookieOptions
					{
						Expires = DateTime.UtcNow.AddDays(7), // Set the expiration date for the cookie
						IsEssential = true, // Makes sure the cookie is always stored
						HttpOnly = true, // For security reasons, makes the cookie inaccessible to client-side scripts
						Secure = true, // Ensures the cookie is only sent over HTTPS
						SameSite = SameSiteMode.Strict // Ensures the cookie is only sent with requests from the same site
					};

					HttpContext.Response.Cookies.Append("UserInfo", userInfoJson, cookieOptions);

					return RedirectToPage("/Index");
				}

				ModelState.AddModelError(string.Empty, "Username or Password is incorrect !");
				
			}
			return Page();
		}
	}
}
