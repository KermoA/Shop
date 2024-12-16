using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.ApplicationServices.Services;
using Shop.Core.Domain;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Emails;



namespace Shop
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.Password.RequiredLength = 3;

				options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
				options.Lockout.MaxFailedAccessAttempts = 2;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			})
				.AddEntityFrameworkStores<ShopContext>()
				.AddDefaultTokenProviders()
				.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
				.AddDefaultUI();

			builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
			builder.Services.AddScoped<IFileServices, FileServices>();
			builder.Services.AddScoped<IKindergartensServices, KindergartensServices>();
            builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
			builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
			builder.Services.AddScoped<IChuckNorrisJokesServices, ChuckNorrisJokesServices>();
			builder.Services.AddScoped<IFreeToGamesServices, FreeToGamesServices>();
			builder.Services.AddScoped<ICocktailsServices, CocktailsServices>();
			builder.Services.AddScoped<IOpenWeatherMapServices, OpenWeatherMapServices>();
			builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.AddTransient<ConfirmationEmail>();

            builder.Services.AddDbContext<ShopContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider
				(Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
				RequestPath = "/multipleFileUpload"
			});

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}