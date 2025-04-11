using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_CRUD_API.Data;
using Prueba_Tecnica_CRUD_API.DTOs;
using Prueba_Tecnica_CRUD_API.Models;

namespace Prueba_Tecnica_CRUD_API.Services
{
    public class CursoService : ICursoService
    {
        private readonly ApplicationDbContext _context;

        public CursoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CursoDTO> CreateAsync(CursoCreateDTO cursoCreateDTO)
        {
            var curso = new Curso
            {
                Codigo = cursoCreateDTO.Codigo,
                Nombre = cursoCreateDTO.Nombre,
                Descripcion = cursoCreateDTO.Descripcion,
                ProfesorId = cursoCreateDTO.ProfesorId
            };

            // Guardar en la base de datos
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();

            // Mapeo de entidad a DTO con Id
            var newCurso = new CursoDTO
            {
                Id = curso.Id,
                Codigo = curso.Codigo,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                ProfesorId = curso.ProfesorId
            };
            return newCurso;
        }

        public async Task DeleteAsync(int id)
        {
            // Eliminar un alumno por ID con SP
            var filasAfectadas = await _context.Database.ExecuteSqlRawAsync("EXEC EliminarCurso @p0", id);

            if (filasAfectadas == 0)
            {
                throw new Exception("Curso no encontrado o ya eliminado");
            }
        }

        public async Task<List<CursoDTO>> GetAllAsync()
        {
            return await _context.Cursos
                .Select(c => new CursoDTO
                {
                    Id = c.Id,
                    Codigo = c.Codigo,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    ProfesorId = c.ProfesorId
                })
                .ToListAsync();
        }

        public async Task<CursoDTO> GetByIdAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                throw new Exception("Curso no encontrado");
            }

            return new CursoDTO
            {
                Id = curso.Id,
                Codigo = curso.Codigo,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                ProfesorId = curso.ProfesorId
            };
        }

        public async Task UpdateAsync(CursoDTO cursoCreateDTO)
        {
            var curso = await _context.Cursos.FindAsync(cursoCreateDTO.Id);
            if (curso == null)
            {
                throw new Exception("Curso no encontrado");
            }

            // Actualizar los campos del curso
            curso.Codigo = cursoCreateDTO.Codigo;
            curso.Nombre = cursoCreateDTO.Nombre;
            curso.Descripcion = cursoCreateDTO.Descripcion;
            curso.ProfesorId = cursoCreateDTO.ProfesorId;

            // Guardar los cambios en la base de datos
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }
    }
}
