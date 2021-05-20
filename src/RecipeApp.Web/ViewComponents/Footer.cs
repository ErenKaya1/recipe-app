using Microsoft.AspNetCore.Mvc;

namespace src.RecipeApp.Web.ViewComponents
{
    public class Footer : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}