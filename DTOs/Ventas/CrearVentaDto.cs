namespace InventarioAPI.DTOs.Ventas
{
    public class CrearVentaDto
    {
        public List<CrearDetalleVentaDto> Detalles { get; set; } = new();
    }
}
