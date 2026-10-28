using InventarioAPI.Models;
using InventarioAPI.DTOs.Productos;
using InventarioAPI.DTOs.Compartidos;

namespace InventarioAPI.Interfaces
{
    public interface IProductoService
    {
        Task<PaginacionDto<ProductoDto>> ObtenerTodosAsync(BuscarProductoDto filtros);

        Task<ProductoDto> ObtenerPorIdAsync(int id);

        Task<ProductoDto> CrearAsync(CrearProductoDto dto);

        Task ActualizarAsync(int id, ActualizarProductoDto dto);

        Task EliminarAsync(int id);
    }
}
