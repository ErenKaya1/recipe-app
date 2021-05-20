using System;
using RecipeApp.Entity.Entities;

namespace RecipeApp.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IMongoRepository<Recipe> RecipeRepository { get; }
        IMongoRepository<Category> CategoryRepository { get; }
    }
}