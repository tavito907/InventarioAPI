using InventarioAPI.DTOs.Categorias;
using InventarioAPI.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> Crear(CrearCategoriaDto dto)
        {
            var resultado = await _service.CrearAsync(dto);
            return Ok(resultado);
        }
    }
}
