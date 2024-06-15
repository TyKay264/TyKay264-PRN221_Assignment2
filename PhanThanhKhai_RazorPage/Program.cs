using BusinessObjects;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PhanThanhKhai_RazorPage
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddDbContext<FunewsManagementDbContext>();
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
			options.LoginPath = "/Login";
			});
			builder.Services.AddSession();
			builder.Services.AddDistributedMemoryCache(); // For session state
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.UseSession();

			app.MapRazorPages();

			app.Run();
		}
	}
}
