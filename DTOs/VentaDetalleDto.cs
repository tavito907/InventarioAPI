namespace InventarioAPI.DTOs
{
    public class VentaDetalleDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDto> Detalles { get; set; } = new();
    }
}
