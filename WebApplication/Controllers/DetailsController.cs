using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
