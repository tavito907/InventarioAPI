namespace InventarioAPI.DTOs
{
    public class DetalleVentaDto
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

    }
}
