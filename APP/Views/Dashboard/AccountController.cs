using Microsoft.AspNetCore.Mvc;

namespace APP.Views.Dashboard
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
