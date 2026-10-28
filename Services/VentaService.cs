using InventarioAPI.Data;
using InventarioAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using InventarioAPI.Models;
using InventarioAPI.DTOs.Ventas;
using InventarioAPI.Exceptions;
using System.Collections.Immutable;
using Microsoft.VisualBasic;
using InventarioAPI.Repositories;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace InventarioAPI.Services
{
    public class VentaService : IVentaService
    {
        private readonly AppDbContext _context;
        private readonly IProductoRepository _productoRepository;

        public VentaService(AppDbContext context, IProductoRepository productoRepository)
        {
            _context = context;
            _productoRepository = productoRepository;
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
            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var venta = new Venta
                {
                    Fecha = DateTime.UtcNow
                };

                var productosIds = dto.Detalles
                    .Select(p => p.ProductoId)
                    .Distinct()
                    .ToList();

                var productos = await _productoRepository
                    .ObtenerPorIdsAsync(productosIds);

                ValidarProductos(dto.Detalles, productos);

                venta.DetallesVenta = CrearDetalles(
                    dto.Detalles,
                    productos,
                    out decimal total);

                venta.Total = total;

                await _context.Ventas.AddAsync(venta);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return venta;
            }
            catch(DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();

                throw new ConflictException(
                    "Uno de los productos fue modificado por otro proceso. Intenta nuevamente.");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private void ValidarProductos(List<CrearDetalleVentaDto> detalles, List<Producto> productos)
        {
            var idsFaltantes = detalles.Select(d => d.ProductoId)
                .Distinct()
                .Except(productos.Select(p => p.Id))
                .ToList();

            if(idsFaltantes.Any())
            {
                throw new NotFoundException($"Los siguientes productos no existen: {string.Join(",", idsFaltantes)}");
            }

            foreach (var detalleDto in detalles)
            {
                var producto = productos.First(p => p.Id == detalleDto.ProductoId);

                if (producto.Stock < detalleDto.Cantidad)
                {
                    throw new BadRequestException($"Stock insuficiente para el producto {producto.Nombre}");
                }
            }
        }

        private List<DetalleVenta> CrearDetalles(List<CrearDetalleVentaDto> detallesDto,
            List<Producto> productos,
            out decimal total)
        {
            total = 0;

            var detalles = new List<DetalleVenta>();

            foreach(var detalleDto in detallesDto)
            {
                var producto = productos.First(p => p.Id == detalleDto.ProductoId);

                var detalle = new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = producto.Precio,
                };

                producto.Stock -= detalleDto.Cantidad;
                producto.Version++;

                detalles.Add(detalle);

                total += producto.Precio * detalleDto.Cantidad;
            }

            return detalles;
        }
    }
}
