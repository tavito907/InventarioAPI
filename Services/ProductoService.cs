using InventarioAPI.Data;
using InventarioAPI.DTOs;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .AsNoTracking()
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                })
                .ToListAsync();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Producto> CrearAsync(CrearProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
            };

            await _context.Productos.AddAsync(producto);

            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);

            if(producto is null)
            {
                return false;
            }

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto is null)
            {
                return false;
            }

            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
