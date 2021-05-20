using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeApp.Entity.Entities;

namespace RecipeApp.Data.Context
{
    public class RecipeContext : IdentityDbContext<User, Role, int>
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}