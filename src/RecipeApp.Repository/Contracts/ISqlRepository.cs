using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Entity.Entities.Base;
using RecipeApp.Repository.Contracts.Base;

namespace RecipeApp.Repository.Contracts
{
    public interface ISqlRepository<T> : IRepository<T> where T : SqlEntity
    {
        Task DeleteRangeAsync(List<T> entities);
    }
}