using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_CRUD_API.DTOs;
using Prueba_Tecnica_CRUD_API.Services;

namespace Prueba_Tecnica_CRUD_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        // Inyeccion de dependencias
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alumnos = await _alumnoService.GetAllAsync();
            if (alumnos == null)
            {
                return NotFound("No se encontraron alumnos.");
            }
            // Crear una respuesta 200 OK con la lista de alumnos
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alumno = await _alumnoService.GetByIdAsync(id);
            if (alumno == null)
            {
                return NotFound($"No se encontró el alumno con ID {id}.");
            }
            return Ok(alumno);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlumnoCreateDTO alumnoDTO)
        {
            try
            {
                var resultado = await _alumnoService.CreateAsync(alumnoDTO);

                // Crear una respuesta 201 Created con la ubicación del nuevo recurso
                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.Id },
                    resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el alumno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AlumnoDTO alumnoDTO)
        {
            // Asignar el ID del alumnoDTO al ID del alumno a actualizar
            var alumno = await _alumnoService.GetByIdAsync(id);
            if (alumno == null)
            {
                return NotFound($"No se encontró el alumno con ID {id}.");
            }

            try
            {
                //Actualizar el alumno en la base de datos
                await _alumnoService.UpdateAsync(alumnoDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el alumno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var alumno = await _alumnoService.GetByIdAsync(id);
                if (alumno == null)
                {
                    return NotFound($"No se encontró el alumno con ID {id}.");
                }
                // Eliminar el alumno de la base de datos
                await _alumnoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el alumno: {ex.Message}");
            }
        }
    }
}