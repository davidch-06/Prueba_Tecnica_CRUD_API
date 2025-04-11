using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_CRUD_API.DTOs;
using Prueba_Tecnica_CRUD_API.Services;

namespace Prueba_Tecnica_CRUD_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesorController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profesores = await _profesorService.GetAllAsync();
            if (profesores == null)
            {
                return NotFound("No se encontraron profesores.");
            }
            // Crear una respuesta 200 OK con la lista de profesores
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profesor = await _profesorService.GetByIdAsync(id);
            if (profesor == null)
            {
                return NotFound($"No se encontró el profesor con ID {id}.");
            }
            // Crear una respuesta 200 OK con el profesor encontrado
            return Ok(profesor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProfesorCreateDTO profesorDTO)
        {
            try
            {
                var resultado = await _profesorService.CreateAsync(profesorDTO);
                // Crear una respuesta 201 Created con la ubicación del nuevo recurso
                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.Id },
                    resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el profesor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProfesorDTO profesorDTO)
        {
            var profesor = await _profesorService.GetByIdAsync(id);
            if (profesor == null)
            {
                return NotFound($"No se encontró el profesor con ID {id}.");
            }

            try
            {
                await _profesorService.UpdateAsync(profesorDTO);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el profesor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            try
            {
                var profesor = await _profesorService.GetByIdAsync(id);
                if (profesor == null)
                {
                    return NotFound($"No se encontró el profesor con ID {id}.");
                }
                await _profesorService.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el profesor: {ex.Message}");
            }
        }
    }
}
