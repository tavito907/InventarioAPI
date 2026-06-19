using InventarioAPI.Data;
using InventarioAPI.DTOs.Ventas;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase 
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta(CrearVentaDto dto)
        {
            try
            {
                var venta = await _ventaService.CrearAsync(dto);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVentas()
        {
            var ventas = await _ventaService.ObtenerTodasAsync();

            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVenta(int id)
        {
            var venta = await _ventaService.ObtenerPorIdAsync(id);

            if(venta is null)
            {
                return NotFound();
            }

            return Ok(venta);
        }
    }
}
