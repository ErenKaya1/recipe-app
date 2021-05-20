using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RecipeApp.Entity.Entities
{
    [Table("user")]
    public class User : IdentityUser<int>
    {
        [NotMapped]
        public override int AccessFailedCount { get; set; }

        [NotMapped]
        public override bool EmailConfirmed { get; set; }

        [NotMapped]
        public override bool LockoutEnabled { get; set; }

        [NotMapped]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }

        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }

        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }
    }
}