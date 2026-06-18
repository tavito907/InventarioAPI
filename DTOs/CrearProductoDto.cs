namespace InventarioAPI.DTOs
{
    public class CrearProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock {  get; set; }

    }
}
