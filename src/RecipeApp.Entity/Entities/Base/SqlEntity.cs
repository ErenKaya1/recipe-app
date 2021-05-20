using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Entity.Entities.Base
{
    public class SqlEntity : IBaseEntity
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        public string Id { get; set; }
    }
}