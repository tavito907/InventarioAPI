using InventarioAPI.Models;
using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ObtenerTodosAsync();

        Task<ProductoDto?> ObtenerPorIdAsync(int id);

        Task<Producto> CrearAsync(CrearProductoDto dto);

        Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto);

        Task<bool> EliminarAsync(int id);
    }
}
