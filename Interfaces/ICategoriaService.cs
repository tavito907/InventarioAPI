using InventarioAPI.DTOs.Categorias;
using InventarioAPI.DTOs.Productos;

namespace InventarioAPI.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> CrearAsync(CrearCategoriaDto dto);
        Task<List<CategoriaDto>> ObtenerTodasAsync();
        Task<CategoriaDetalleDto?> ObtenerPorIdAsync(int id);
        Task EliminarAsync(int id);
        Task ActualizarAsync(int id, ActualizarCategoriaDto dto);
    }
}
