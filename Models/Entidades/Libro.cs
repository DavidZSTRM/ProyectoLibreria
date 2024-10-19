using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoLibreria.Models.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; }
        public Autor Autor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un autor.")]
        public int AutorId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Categorias { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Autores { get; set; }
    }
}
