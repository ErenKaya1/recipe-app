using RecipeApp.Entity.Entities.Base;

namespace RecipeApp.Entity.Entities
{
    public class Category : MongoEntity
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Slug { get; set; }
    }
}