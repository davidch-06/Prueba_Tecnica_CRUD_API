using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_CRUD_API.Data;
using Prueba_Tecnica_CRUD_API.DTOs;
using Prueba_Tecnica_CRUD_API.Models;

namespace Prueba_Tecnica_CRUD_API.Services
{
    public class ProfesorService : IProfesorService
    {
        //Inyeccion de dependencias
        private readonly ApplicationDbContext _context;

        public ProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfesorDTO> CreateAsync(ProfesorCreateDTO profesorCreateDTO)
        {
            // Mapeo de DTO a entidad
            var profesor = new Profesor
            {
                Nombre = profesorCreateDTO.Nombre,
                Apellido = profesorCreateDTO.Apellido,
                NumeroIdentificacion = profesorCreateDTO.NumeroIdentificacion
            };

            // Guardar en la base de datos
            await _context.Profesores.AddAsync(profesor);
            await _context.SaveChangesAsync();

            // Mapeo de entidad a DTO con Id
            var newProfesor = new ProfesorDTO
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                NumeroIdentificacion = profesor.NumeroIdentificacion
            };
            return newProfesor;
        }

        public async Task DeleteAsync(int id)
        {
            var profesor = _context.Profesores.Find(id);

            if (profesor == null)
            {
                throw new Exception("Profesor no encontrado");
            }

            // Eliminar el profesor encontrado
            _context.Profesores.Remove(profesor);
            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProfesorDTO>> GetAllAsync()
        {
            // Obtener todos los alumnos y mapear a DTO
            return await _context.Profesores
                .Select(p => new ProfesorDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    NumeroIdentificacion = p.NumeroIdentificacion
                })
                .ToListAsync();
        }

        public async Task<ProfesorDTO> GetByIdAsync(int id)
        {
            // Buscar por ID en la base de datos
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                throw new Exception("Profesor no encontrado");
            }

            // Mapeo de entidad a DTO
            return new ProfesorDTO
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                NumeroIdentificacion = profesor.NumeroIdentificacion
            };
        }

        public async Task UpdateAsync(ProfesorDTO profesorCreateDTO)
        {
            var profesor = await _context.Profesores.FindAsync(profesorCreateDTO.Id);

            if (profesor == null)
            {
                throw new Exception("Profesor no encontrado");
            }
            // Actualizar los campos del profesor
            profesor.Nombre = profesorCreateDTO.Nombre;
            profesor.Apellido = profesorCreateDTO.Apellido;
            profesor.NumeroIdentificacion = profesorCreateDTO.NumeroIdentificacion;

            // Guardar los cambios en la base de datos
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();
        }
    }
}
