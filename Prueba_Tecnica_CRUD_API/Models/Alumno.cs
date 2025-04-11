using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_CRUD_API.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }


        // Relación con Cursos (N cursos)
        public IEnumerable<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
