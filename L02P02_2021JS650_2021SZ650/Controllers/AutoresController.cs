using L02P02_2021JS650_2021SZ650.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2021JS650_2021SZ650.Controllers
{
    public class AutoresController : Controller
    {
        private readonly LibreriaDbContext _libreriaDbContext;

        public AutoresController(LibreriaDbContext libreriaDbContext)
        {
            _libreriaDbContext = libreriaDbContext;
        }

        public IActionResult Index()
        {
            var listadoDeAutores = (from e in _libreriaDbContext.Autores
                                    select new
                                    {
                                        nombreautor = e.Autor,
                                    }).ToList();
            ViewData["listadoAutores"] = listadoDeAutores;

            return View();
        }
    }
}
