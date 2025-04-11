using Prueba_Tecnica_CRUD_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_CRUD_API.DTOs
{
    public class CursoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ProfesorId { get; set; }
    }
}
