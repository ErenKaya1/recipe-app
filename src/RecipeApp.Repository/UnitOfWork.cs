using System;
using Microsoft.Extensions.Options;
using RecipeApp.Core.Models;
using RecipeApp.Entity.Entities;
using RecipeApp.Repository.Contracts;

namespace RecipeApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly IOptions<MongoDbSettings> _mongoDbSettings;
        private IMongoRepository<Recipe> _recipeRepository;
        private IMongoRepository<Category> _categoryRepository;

        public IMongoRepository<Recipe> RecipeRepository => _recipeRepository ??= new MongoRepository<Recipe>(_mongoDbSettings);
        public IMongoRepository<Category> CategoryRepository => _categoryRepository ??= new MongoRepository<Category>(_mongoDbSettings);

        public UnitOfWork(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}