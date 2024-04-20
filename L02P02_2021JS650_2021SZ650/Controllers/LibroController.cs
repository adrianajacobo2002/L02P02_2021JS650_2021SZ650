using Microsoft.AspNetCore.Mvc;

namespace L02P02_2021JS650_2021SZ650.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
