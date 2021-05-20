using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RecipeApp.Data.Context;

namespace RecipeApp.Data.Factory
{
    public class RecipeContextFactory : IDesignTimeDbContextFactory<RecipeContext>
    {
        public RecipeContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RecipeContext>();
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "../RecipeApp.Web");
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(configPath)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            var connectionString = configuration.GetConnectionString("PostgreSqlProvider");

            builder.UseNpgsql(connectionString);
            return new RecipeContext(builder.Options);
        }
    }
}