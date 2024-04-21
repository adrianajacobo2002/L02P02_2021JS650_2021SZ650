using L02P02_2021JS650_2021SZ650.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2021JS650_2021SZ650.Controllers
{
    public class LibroController : Controller
    {
        private readonly LibreriaDbContext _libreriaDbContext;

        public LibroController(LibreriaDbContext libreriaDbContext)
        {
            _libreriaDbContext = libreriaDbContext;
        }

        public IActionResult Index(int id)
        {
            string nombreAutor = _libreriaDbContext.Autores.FirstOrDefault(x => x.Id == id)?.Autor ?? "";
            var libros = (from l in _libreriaDbContext.Libros
                          join a in _libreriaDbContext.Autores on l.IdAutor equals a.Id
                          where l.IdAutor == id
                          select new {
                            id = l.Id,
                            nombre = l.Nombre
                          }).ToList();

            ViewData["autor"] = nombreAutor;
            ViewData["libros"] = libros;
            return View();
        }
    }
}
