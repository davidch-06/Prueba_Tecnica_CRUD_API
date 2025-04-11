using Prueba_Tecnica_CRUD_API.DTOs;

namespace Prueba_Tecnica_CRUD_API.Services
{
    public interface ICursoService
    {
        //Implementar los metodos del CRUD de alumnos
        Task<List<CursoDTO>> GetAllAsync();
        Task<CursoDTO> GetByIdAsync(int id);
        Task<CursoDTO> CreateAsync(CursoCreateDTO cursoCreateDTO);
        Task UpdateAsync(CursoDTO cursoCreateDTO);
        Task DeleteAsync(int id);
    }
}
