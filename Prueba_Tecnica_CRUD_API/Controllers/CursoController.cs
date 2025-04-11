using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_CRUD_API.DTOs;
using Prueba_Tecnica_CRUD_API.Services;

namespace Prueba_Tecnica_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cursos = await _cursoService.GetAllAsync();
            if (cursos == null)
            {
                return NotFound("No se encontraron cursos.");
            }
            // Crear una respuesta 200 OK con la lista de cursos
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound($"No se encontró el curso con ID {id}.");
            }
            // Crear una respuesta 200 OK con el curso encontrado
            return Ok(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CursoCreateDTO cursoDTO)
        {
            try
            {
                var resultado = await _cursoService.CreateAsync(cursoDTO);
                // Crear una respuesta 201 Created con la ubicación del nuevo recurso
                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.Id },
                    resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el curso: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CursoDTO cursoDTO)
        {
            var cursoExistente = await _cursoService.GetByIdAsync(id);
            if (cursoExistente == null)
            {
                return NotFound($"No se encontró el curso con ID {id}.");
            }
            try
            {
                await _cursoService.UpdateAsync(cursoDTO);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el curso: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cursoExistente = await _cursoService.GetByIdAsync(id);
            if (cursoExistente == null)
            {
                return NotFound($"No se encontró el curso con ID {id}.");
            }
            try
            {
                await _cursoService.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el curso: {ex.Message}");
            }
        }
    }
}