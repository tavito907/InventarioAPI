using InventarioAPI.DTOs.Categorias;
using InventarioAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Crear(CrearCategoriaDto dto)
        {
            var resultado = await _service.CrearAsync(dto);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var categorias = await _service.ObtenerTodasAsync();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var categoria = await _service.ObtenerPorIdAsync(id);

            if(categoria is null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            await _service.EliminarAsync(id);

            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, ActualizarCategoriaDto dto)
        {

             await _service.ActualizarAsync(id, dto);

             return NoContent();

        }
    }
}
