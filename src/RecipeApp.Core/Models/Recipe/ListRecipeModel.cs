namespace RecipeApp.Core.Models.Recipe
{
    public class ListRecipeModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public bool OnHomepage { get; set; }
        public string Slug { get; set; }
        public string ImageName { get; set; }
    }
}