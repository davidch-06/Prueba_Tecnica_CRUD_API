using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_CRUD_API.Data;
using Prueba_Tecnica_CRUD_API.DTOs;
using Prueba_Tecnica_CRUD_API.Models;

namespace Prueba_Tecnica_CRUD_API.Services
{
    public class AlumnoService : IAlumnoService
    {
        // Inyeccion de dependencias
        private readonly ApplicationDbContext _context;

        public AlumnoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AlumnoDTO> CreateAsync(AlumnoCreateDTO alumnoCreateDTO)
        {
            // Mapeo de DTO a entidad
            var alumno = new Alumno
            {
                Nombre = alumnoCreateDTO.Nombre,
                Apellido = alumnoCreateDTO.Apellido,
                FechaNacimiento = alumnoCreateDTO.FechaNacimiento,
                NumeroIdentificacion = alumnoCreateDTO.NumeroIdentificacion
            };

            // Guardar en la base de datos
            await _context.Alumnos.AddAsync(alumno);
            await _context.SaveChangesAsync();

            // Mapeo de entidad a DTO con Id
            var newAlumno = new AlumnoDTO
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                FechaNacimiento = alumno.FechaNacimiento,
                NumeroIdentificacion = alumno.NumeroIdentificacion
            };
            return newAlumno;
        }

        public async Task DeleteAsync(int id)
        {

            // Buscar el alumno por ID en la base de datos
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                throw new Exception("Alumno no encontrado");
            }

            // Eliminar el profesor encontrado
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AlumnoDTO>> GetAllAsync()
        {
            // Obtener todos los alumnos y mapear a DTO
            return await _context.Alumnos
                 .Select(a => new AlumnoDTO
                 {
                     Id = a.Id,
                     Nombre = a.Nombre,
                     Apellido = a.Apellido,
                     FechaNacimiento = a.FechaNacimiento,
                     NumeroIdentificacion = a.NumeroIdentificacion
                 })
                 .ToListAsync();
        }

        public async Task<AlumnoDTO> GetByIdAsync(int id)
        {
            // Buscar por ID en la base de datos
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                throw new Exception("Alumno no encontrado");
            }

            return new AlumnoDTO
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                FechaNacimiento = alumno.FechaNacimiento,
                NumeroIdentificacion = alumno.NumeroIdentificacion
            };
        }

        public async Task UpdateAsync(AlumnoDTO alumnoCreateDTO)
        {
            var alumno = await _context.Alumnos.FindAsync(alumnoCreateDTO.Id);

            if (alumno == null)
            {
                throw new Exception("Alumno no encontrado");
            }

            // Actualizar los campos del alumno
            alumno.Nombre = alumnoCreateDTO.Nombre;
            alumno.Apellido = alumnoCreateDTO.Apellido;
            alumno.FechaNacimiento = alumnoCreateDTO.FechaNacimiento;
            alumno.NumeroIdentificacion = alumnoCreateDTO.NumeroIdentificacion;

            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();
        }
    }
}
