using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RecipeApp.Entity.Entities
{
    [Table("role")]
    public class Role : IdentityRole<int>
    {

    }
}