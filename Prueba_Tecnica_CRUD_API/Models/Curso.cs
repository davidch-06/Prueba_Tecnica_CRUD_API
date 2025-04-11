using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_CRUD_API.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        // Relación con Profesor (1 Curso -> 1 Profesor)
        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        // Relación con Alumnos (1 Curso -> Muchos Alumnos)
        public ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
    }
}
