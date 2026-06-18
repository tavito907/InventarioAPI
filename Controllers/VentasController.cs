using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase 
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta()
        {
            var venta = new Venta
            {
                Fecha = DateTime.UtcNow,
                Total = 850
            };

            venta.DetallesVenta.Add(
                new DetalleVenta
                {
                    ProductoId = 1,
                    Cantidad = 2,
                    PrecioUnitario = 350
                });

            venta.DetallesVenta.Add(
                new DetalleVenta
                {
                    ProductoId = 3,
                    Cantidad = 1,
                    PrecioUnitario = 150
                });

            await _context.Ventas.AddAsync(venta);

            await _context.SaveChangesAsync();

            return Ok(venta);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVentas()
        {
            var ventas = await _context.Ventas.AsNoTracking().Select(v => new VentaDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
            }).ToListAsync();

            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVenta(int id)
        {
            var venta = await _context.Ventas.AsNoTracking().Where(v => v.Id == id).Select(v => new VentaDetalleDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,

                Detalles = v.DetallesVenta.Select(d => new DetalleVentaDto
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()

            }).FirstOrDefaultAsync();

            if (venta is null)
            {
                return NotFound();
            }

            return Ok(venta);
        }
    }
}
