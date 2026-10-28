using InventarioAPI.Data;
using InventarioAPI.DTOs.Error;
using InventarioAPI.DTOs.Productos;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Controllers
{

    public class ProductosController : BaseApiController
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene una lista paginada de productos aplicando filtros opcionales
        /// </summary>
        /// <param name="filtros">
        /// Filtros opcionales para buscar, ordenar y paginar productos.
        /// </param>
        /// <returns>
        /// Lista de productos que cumplen los filtros especificados.
        /// </returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerProductos([FromQuery] BuscarProductoDto filtros)
        {
            var productos = await _productoService.ObtenerTodosAsync(filtros);

            return Ok(productos);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CrearProducto(CrearProductoDto dto)
        {
            var producto = await _productoService.CrearAsync(dto);

            return Ok(producto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if(producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ActualizarProductoDto dto)
        {
            await _productoService.ActualizarAsync(id, dto);

            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            await _productoService.EliminarAsync(id);

            return NoContent();
        }
    }
}
