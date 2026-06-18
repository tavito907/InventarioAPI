namespace InventarioAPI.DTOs
{
    public class ActualizarProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock {  get; set; }

    }
}
