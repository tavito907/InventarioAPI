namespace InventarioAPI.DTOs
{
    public class CrearVentaDto
    {
        public List<CrearDetalleVentaDto> Detalles { get; set; } = new();
    }
}
