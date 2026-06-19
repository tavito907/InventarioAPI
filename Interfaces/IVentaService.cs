using InventarioAPI.Models;
using InventarioAPI.DTOs;

namespace InventarioAPI.Interfaces
{
    public interface IVentaService
    {
        Task<List<VentaDto>> ObtenerTodasAsync();

        Task<VentaDetalleDto?> ObtenerPorIdAsync(int id);

        Task<Venta> CrearAsync(CrearVentaDto dto);
    }
}
