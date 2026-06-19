using InventarioAPI.Data;
using InventarioAPI.DTOs.Categorias;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;

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
    }
}
