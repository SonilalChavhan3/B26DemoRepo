using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
