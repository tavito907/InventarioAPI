using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using InventarioAPI.Models;

namespace InventarioAPI.Services
{
    public class VentaService : IVentaService
    {
        private readonly AppDbContext _context;

        public VentaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VentaDto>> ObtenerTodasAsync()
        {
            return await _context.Ventas.AsNoTracking().Select(v => new VentaDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
            }).ToListAsync();
        }

        public async Task<VentaDetalleDto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Ventas.AsNoTracking().Where(v => v.Id == id).Select(v => new VentaDetalleDto
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
        }

        public async Task<Venta> CrearAsync(CrearVentaDto dto)
        {
            var venta = new Venta
            {
                Fecha = DateTime.UtcNow
            };

            decimal total = 0;
            foreach (var detalledDto in dto.Detalles)
            {
                var producto = await _context.Productos.FindAsync(detalledDto.ProductoId);

                if(producto is null)
                {
                    throw new Exception("Producto no encontrado");
                }

                if (producto.Stock < detalledDto.Cantidad)
                {
                    throw new Exception(
                        $"Stock insuficiente para el producto {producto.Nombre}");
                }

                var detalleVenta = new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = detalledDto.Cantidad,
                    PrecioUnitario = producto.Precio
                };

                producto.Stock -= detalledDto.Cantidad;

                venta.DetallesVenta.Add(detalleVenta);

                total += producto.Precio * detalledDto.Cantidad;
            }

            venta.Total = total;

            await _context.Ventas.AddAsync(venta);
            await _context.SaveChangesAsync();

            return venta;
        }
    }
}
