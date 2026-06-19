using InventarioAPI.Data;
using InventarioAPI.Models;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioAPI.Interfaces;
using InventarioAPI.DTOs.Productos;

namespace InventarioAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerTodosAsync();

            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto(CrearProductoDto dto)
        {
            var producto = await _productoService.CrearAsync(dto);

            return Ok(producto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if(producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ActualizarProductoDto dto)
        {
            var actualizado = await _productoService.ActualizarAsync(id, dto);

            if(!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var eliminado = await _productoService.EliminarAsync(id);

            if(!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
