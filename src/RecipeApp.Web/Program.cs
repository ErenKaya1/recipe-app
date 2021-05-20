using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeApp.Entity.Entities;

namespace RecipeApp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                if (await roleManager.FindByNameAsync("admin") == null)
                {
                    await roleManager.CreateAsync(new Role
                    {
                        Name = "admin"
                    });

                    await roleManager.CreateAsync(new Role
                    {
                        Name = "moderator"
                    });

                    var user = new User
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                    };

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                    await userManager.CreateAsync(user, "asd123");
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
