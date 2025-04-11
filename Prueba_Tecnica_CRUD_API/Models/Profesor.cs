using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_CRUD_API.Models
{
    public class Profesor
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }


        // Relación con Cursos (1 profesor puede impartir muchos cursos)
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
