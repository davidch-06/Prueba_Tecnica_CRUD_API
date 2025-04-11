using Prueba_Tecnica_CRUD_API.DTOs;

namespace Prueba_Tecnica_CRUD_API.Services
{
    public interface IProfesorService
    {
        //Implementar los metodos del CRUD de alumnos
        Task<List<ProfesorDTO>> GetAllAsync();
        Task<ProfesorDTO> GetByIdAsync(int id);
        Task<ProfesorDTO> CreateAsync(ProfesorCreateDTO profesorCreateDTO);
        Task UpdateAsync(ProfesorDTO profesorCreateDTO);
        Task DeleteAsync(int id);
    }
}
