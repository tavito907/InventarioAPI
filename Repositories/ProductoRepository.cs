using InventarioAPI.Data;
using InventarioAPI.DTOs.Compartidos;
using InventarioAPI.DTOs.Productos;
using InventarioAPI.Enums;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginacionDto<ProductoDto>> ObtenerTodosAsync(BuscarProductoDto filtros)
        {
            var consulta = _context.Productos.AsNoTracking().AsQueryable();

            if(!string.IsNullOrWhiteSpace(filtros.Nombre))
            {
                consulta = consulta.Where(p =>
                EF.Functions.ILike(
                    p.Nombre,
                    $"%{filtros.Nombre}%"));
            }

            if (filtros.PrecioMin.HasValue)
            {
                consulta = consulta.Where(p => p.Precio >= filtros.PrecioMin.Value);
            }

            if (filtros.PrecioMax.HasValue)
            {
                consulta = consulta.Where(p => p.Precio <= filtros.PrecioMax.Value);
            }

            if (filtros.CategoriaId.HasValue)
            {
                consulta = consulta.Where(p => p.CategoriaId == filtros.CategoriaId.Value);
            }

            if (filtros.OrdenarPor.HasValue)
            {
                switch (filtros.OrdenarPor.Value)
                {
                    case OrdenProducto.Nombre:

                        consulta = filtros.Descendente ? consulta.OrderByDescending(p => p.Nombre)
                             : consulta.OrderBy(p => p.Nombre);

                        break;

                    case OrdenProducto.Precio:

                        consulta = filtros.Descendente ? consulta.OrderByDescending(p => p.Precio)
                            : consulta.OrderBy(p => p.Precio);

                        break;

                    case OrdenProducto.Stock:

                        consulta = filtros.Descendente ? consulta.OrderByDescending(p => p.Stock)
                            : consulta.OrderBy(p => p.Stock);

                        break;
                }
            }

            var totalRegistros = await consulta.CountAsync();

            var totalPaginas = (int)Math.Ceiling(
                (double)totalRegistros / filtros.TamanoPagina);

            consulta = consulta.Skip((filtros.Pagina - 1) * filtros.TamanoPagina)
                    .Take(filtros.TamanoPagina);

            var productos = await consulta.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                Categoria = p.Categoria.Nombre
            }).ToListAsync();

            return new PaginacionDto<ProductoDto>
            {
                Pagina = filtros.Pagina,
                TamanoPagina = filtros.TamanoPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = totalPaginas,
                Datos = productos
            };
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

        public async Task<Producto?> ObtenerEntidadPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AgregarAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
        }

        public Task ActualizarAsync(Producto producto)
        {
            _context.Productos.Update(producto);

            return Task.CompletedTask;
        }

        public Task EliminarAsync(Producto producto)
        {
            _context.Productos.Remove(producto);

            return Task.CompletedTask;
        }

        public async Task<Categoria?> ObtenerCategoriaAsync(int categoriaId)
        {
            return await _context.Categorias.FindAsync(categoriaId);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Producto>> ObtenerPorIdsAsync(List<int> ids)
        {
            return await _context.Productos.Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}
