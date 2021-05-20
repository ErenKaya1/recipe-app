using System;
using RecipeApp.Entity.Entities.Base;

namespace RecipeApp.Entity.Entities
{
    public class Recipe : MongoEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string[] Ingredients { get; set; }
        public string[] Directions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ImageName { get; set; }
        public Category Category { get; set; }
        public bool OnHomepage { get; set; }
        public int Time { get; set; }
        public int Difficulty { get; set; }
        public int Servings { get; set; }
    }
}