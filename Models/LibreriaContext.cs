using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Models
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
