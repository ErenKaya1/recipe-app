using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeApp.Data.Context;
using RecipeApp.Core.Models;
using RecipeApp.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using RecipeApp.Core.Models.Moderator;
using RecipeApp.Web.CustomMessages;
using RecipeApp.Service.Contracts;
using RecipeApp.Service.Services;
using RecipeApp.Core.Models.Mail;
using RecipeApp.Entity.DTOs;
using RecipeApp.Core.Models.User;
using RecipeApp.Repository.Contracts;
using RecipeApp.Repository;
using RecipeApp.Core.Models.Category;
using RecipeApp.Core.Models.Recipe;

namespace RecipeApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<RecipeContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlProvider")));
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters += "öçşığÜÖÇŞİĞÜ";
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<RecipeContext>();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = new PathString("/user/login");
                config.LogoutPath = new PathString("/user/logout");
                config.Cookie = new CookieBuilder
                {
                    Name = "user",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax
                };
                config.SlidingExpiration = true;
                config.ExpireTimeSpan = TimeSpan.FromDays(14);
            });

            services.Configure<DataProtectionTokenProviderOptions>(config => config.TokenLifespan = TimeSpan.FromMinutes(15));

            var scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<RecipeContext>())
                dbContext.Database.Migrate();

            services.AddAutoMapper(config =>
            {
                config.CreateMap<NewModeratorModel, User>();
                config.CreateMap<User, NewModeratorModel>();
                config.CreateMap<User, UserDTO>();
                config.CreateMap<UserDTO, User>();
                config.CreateMap<UpdateUserModel, User>();
                config.CreateMap<User, UpdateUserModel>();
                config.CreateMap<NewCategoryModel, Category>();
                config.CreateMap<Category, NewCategoryModel>();
                config.CreateMap<Category, EditCategoryModel>();
                config.CreateMap<EditCategoryModel, Category>();
                config.CreateMap<Recipe, NewRecipeModel>();
                config.CreateMap<NewRecipeModel, Recipe>();
                config.CreateMap<ListRecipeModel, Recipe>();
                config.CreateMap<Recipe, ListRecipeModel>();
                config.CreateMap<Recipe, EditRecipeModel>();
                config.CreateMap<EditRecipeModel, Recipe>();
            });

            services.Configure<MailSettings>(Configuration.GetSection("Mail"));

            services.AddScoped<IMailService>(x => new MailService
            {
                Host = Configuration["Mail:Host"],
                Port = string.IsNullOrEmpty(Configuration["Mail:Port"]) ? 0 : Convert.ToInt32(Configuration["Mail:Port"]),
                Username = Configuration["Mail:Username"],
                Password = Configuration["Mail:Password"],
                UseSsl = !string.IsNullOrEmpty(Configuration["Mail:UseSsl"]) && Convert.ToBoolean(Configuration["Mail:UseSsl"])
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
