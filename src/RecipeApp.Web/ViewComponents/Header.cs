using Microsoft.AspNetCore.Mvc;

namespace src.RecipeApp.Web.ViewComponents
{
    public class Header : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}