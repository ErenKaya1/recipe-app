using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RecipeApp.Entity.Entities;

namespace RecipeApp.Web.Controllers.Base
{
    [Authorize]
    public class BaseAdminController : BaseController
    {
        private UserManager<User> _userManager;
        public UserManager<User> UserManager => _userManager ?? (UserManager<User>)HttpContext?.RequestServices.GetService(typeof(UserManager<User>));

        public int UserId
        {
            get
            {
                var userId = UserManager.GetUserId(User);
                return int.Parse(userId);
            }
        }
    }
}