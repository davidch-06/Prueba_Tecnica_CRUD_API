using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_CRUD_API.DTOs
{
    public class ProfesorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroIdentificacion { get; set; }
    }
}
