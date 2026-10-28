using InventarioAPI.Data;
using InventarioAPI.DTOs.Categorias;
using InventarioAPI.Exceptions;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriaDto> CrearAsync(CrearCategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
            };

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
            };
        }

        public async Task<List<CategoriaDto>> ObtenerTodasAsync()
        {
            return await _context.Categorias.AsNoTracking().Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
            }).ToListAsync();
        }

        public async Task<CategoriaDetalleDto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Categorias
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CategoriaDetalleDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,

                    Productos = c.Productos.Select(p => new ProductoCategoriaDto
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if(categoria is  null)
                throw new NotFoundException("La categoria no existe");

            bool tieneProductos = await _context.Productos.AnyAsync(p => p.CategoriaId == id);

            if(tieneProductos)
            {
                throw new BadRequestException("No se puede eliminar una categoria que tiene productos asociados.");
            }

            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(int id, ActualizarCategoriaDto dto)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria is null)
                throw new NotFoundException("La categoria no existe.");

            categoria.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
        }
    }
}
