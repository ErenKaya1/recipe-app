using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RecipeApp.Entity.Entities.Base;
using RecipeApp.Repository.Contracts.Base;

namespace RecipeApp.Repository.Contracts
{
    public interface IMongoRepository<T> : IRepository<T> where T : MongoEntity
    {
        Task DeleteRangeAsync(Expression<Func<T, bool>> predicate);
    }
}