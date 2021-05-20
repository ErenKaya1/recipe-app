using Microsoft.AspNetCore.Mvc;
using RecipeApp.Web.Controllers.Base;

namespace RecipeApp.Web.Controllers
{
    public class DashboardController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}