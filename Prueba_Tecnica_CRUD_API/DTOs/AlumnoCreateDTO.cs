namespace Prueba_Tecnica_CRUD_API.DTOs
{
    public class AlumnoCreateDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroIdentificacion { get; set; }
    }
}
