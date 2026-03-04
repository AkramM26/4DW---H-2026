using Microsoft.AspNetCore.Mvc;

namespace TP1.Controllers
{
    public class DriversController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
