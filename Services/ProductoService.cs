using InventarioAPI.Data;
using InventarioAPI.DTOs.Productos;
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
                    Categoria = p.Categoria.Nombre,
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
                    Stock = p.Stock,
                    Categoria = p.Categoria.Nombre

                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
        {
            var categoria = await _context.Categorias.FindAsync(dto.CategoriaId);

            if(categoria is null)
            {
                throw new Exception("La categoría no existe");
            }

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                CategoriaId = categoria.Id
            };

            await _context.Productos.AddAsync(producto);

            await _context.SaveChangesAsync();

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Categoria = categoria.Nombre
            };

        }

        public async Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);

            var categoria = await _context.Categorias.FindAsync(dto.CategoriaId);

            if (producto is null)
            {
                throw new Exception("Producto no encontrado");
            }

            if(categoria is null)
            {
                throw new Exception("La categoría no existe");
            }

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;

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
