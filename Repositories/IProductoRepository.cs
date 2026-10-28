using InventarioAPI.DTOs.Compartidos;
using InventarioAPI.DTOs.Productos;
using InventarioAPI.Models;

namespace InventarioAPI.Repositories
{
    public interface IProductoRepository
    {
        Task<PaginacionDto<ProductoDto>> ObtenerTodosAsync(BuscarProductoDto filtros);

        Task<ProductoDto?> ObtenerPorIdAsync(int id);

        Task<Producto?> ObtenerEntidadPorIdAsync(int id);

        Task<Categoria?> ObtenerCategoriaAsync(int categoriaId);




        Task AgregarAsync(Producto producto);

        Task ActualizarAsync(Producto producto);

        Task EliminarAsync(Producto producto);

        Task GuardarCambiosAsync();


        Task<List<Producto>> ObtenerPorIdsAsync(List<int> ids);
    }
}
