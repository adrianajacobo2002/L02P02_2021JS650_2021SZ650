using L02P02_2021JS650_2021SZ650.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2021JS650_2021SZ650.Controllers
{
    public class ComentariosLibroController : Controller
    {
        private readonly LibreriaDbContext _libreriaDbContext;

        public ComentariosLibroController(LibreriaDbContext libreriaDbContext)
        {
            _libreriaDbContext = libreriaDbContext;
        }

        public IActionResult Index(int id)
        {
            var info = (from a in _libreriaDbContext.Autores
                            join l in _libreriaDbContext.Libros on a.Id equals l.IdAutor
                            where l.Id == id
                            select new {
                                autor = a.Autor,
                                libro = l.Nombre,
                                id_libro = l.Id
                            }).FirstOrDefault();

            var comentarios = (from c in _libreriaDbContext.ComentariosLibros
                               join l in _libreriaDbContext.Libros on c.IdLibro equals l.Id
                               where l.Id == id
                               select new
                               {
                                   comentario = c.Comentarios,
                                   usuario = c.Usuario,
                                   fecha = c.CreatedAt
                               }).ToList();

            ViewData["autor"] = info.autor.ToString();
            ViewData["libro"] = info.libro.ToString();
            ViewData["id_libro"] = info.id_libro.ToString();
            ViewData["comentarios"] = comentarios;
            return View();
        }

        public IActionResult AddComment(AddCommentModel model)
        {
            string comment = model.comment;
            string idLibro = model.id_libro;
            string autor = model.autor;

            var comentarios = (from c in _libreriaDbContext.ComentariosLibros
                               join l in _libreriaDbContext.Libros on c.IdLibro equals l.Id
                               where l.Id == int.Parse(idLibro)
                               select new
                               {
                                   id = c.Id,
                                   comentario = c.Comentarios,
                                   usuario = c.Usuario,
                                   fecha = c.CreatedAt
                               }).ToList();

            ComentariosLibro cl = new ComentariosLibro();
            cl.Id = (_libreriaDbContext.ComentariosLibros.OrderBy(cl => cl.Id).LastOrDefault()?.Id ?? 0) + 1;
            cl.Comentarios = comment;
            cl.IdLibro = int.Parse(idLibro);
            cl.Usuario = autor;
            cl.CreatedAt = DateTime.Now;
            _libreriaDbContext.ComentariosLibros.Add(cl);
            _libreriaDbContext.SaveChanges();

            comentarios.Add(new
            {
                id = cl.Id,
                comentario = cl.Comentarios,
                usuario = cl.Usuario,
                fecha = cl.CreatedAt
            });

            var info = (from a in _libreriaDbContext.Autores
                        join l in _libreriaDbContext.Libros on a.Id equals l.IdAutor
                        where l.Id == int.Parse(idLibro)
                        select new
                        {
                            autor = a.Autor,
                            libro = l.Nombre,
                            id_libro = l.Id
                        }).FirstOrDefault();

            ViewData["autor"] = info.autor.ToString();
            ViewData["libro"] = info.libro.ToString();
            ViewData["id_libro"] = idLibro;
            ViewData["comentarios"] = comentarios;
            ViewData["disableForm"] = true; 
            return View("Index");
        }
    }

    public class AddCommentModel ()
    {
        public string comment { get; set; }
        public string id_libro { get; set; }
        public string autor { get; set; }
    }
}
