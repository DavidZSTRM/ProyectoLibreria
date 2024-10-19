using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;
using ProyectoLibreria.Services;

namespace ProyectoLibreria.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibreriaContext _context;
        private readonly IServicioLista _servicioLista;
        private readonly IServicioUsuario _servicioUsuario;

        public LibrosController(LibreriaContext context, IServicioLista servicioLista, IServicioUsuario servicioUsuario)
        {
            _context = context;
            _servicioLista = servicioLista;
            _servicioUsuario = servicioUsuario;
        }

        public async Task<IActionResult> Lista()
        {
            // Obtener la lista de libros y sus autores
            var libros = await _context.Libros
                .Include(l => l.Autor) // Incluir los autores usando el AutorId
                .ToListAsync();

            return View(libros);
        }

        public async Task<IActionResult> Crear()
        {
            Libro libro = new()
            {
                Autores = await _servicioLista.GetListaAutores(), // Obtener lista de autores para el dropdown
            };

            return View(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Libro libro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(libro);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Libro creado exitosamente!!!";
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(String.Empty, "Ha ocurrido un error");
                }
            }
            libro.Autores = await _servicioLista.GetListaAutores();
            return View(libro);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.Autores = await _servicioLista.GetListaAutores();
            return View(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Libro libro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var libroExistente = await _context.Libros.FindAsync(libro.Id);
                    if (libroExistente == null)
                    {
                        return NotFound();
                    }

                    libroExistente.Titulo = libro.Titulo;
                    libroExistente.Autor = await _context.Autores.FindAsync(libro.AutorId);

                    _context.Update(libroExistente);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Libro editado exitosamente!!!";
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error");
                }
            }

            libro.Autores = await _servicioLista.GetListaAutores();
            return View(libro);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            try
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Libro eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrió un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(Lista));
        }
    }
}
