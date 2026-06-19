using InventarioAPI.DTOs.Categorias;

namespace InventarioAPI.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> CrearAsync(CrearCategoriaDto dto);
        
    }
}
