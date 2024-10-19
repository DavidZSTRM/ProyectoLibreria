namespace ProyectoLibreria.Models.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null;
        public string Correo { get; set;} = null;
        public string Clave { get; set; } = null;
        public Libro Libro { get; set; }
    }
}
