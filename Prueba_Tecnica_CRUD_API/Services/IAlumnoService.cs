using Prueba_Tecnica_CRUD_API.DTOs;

namespace Prueba_Tecnica_CRUD_API.Services
{
    public interface IAlumnoService
    {
        //Implementar los metodos del CRUD de alumnos
        Task<List<AlumnoDTO>> GetAllAsync();
        Task<AlumnoDTO> GetByIdAsync(int id);
        Task<AlumnoDTO> CreateAsync(AlumnoCreateDTO alumnoCreateDTO);
        Task UpdateAsync(AlumnoDTO alumnoCreateDTO);
        Task DeleteAsync(int id);

        Task<List<AlumnoCursoProfesorDTO>> ObtenerAlumnosPorCursoAsync(int cursoId);
        Task<List<CursoAlumnosDTO>> ObtenerCursosYAlumnosPorProfesorAsync(int profesorId);

    }
}
