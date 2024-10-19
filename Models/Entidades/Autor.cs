using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        public Autor() { }
    }
}
