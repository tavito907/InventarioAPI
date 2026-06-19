namespace InventarioAPI.DTOs.Productos
{
    public class ActualizarProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock {  get; set; }
        public int CategoriaId { get; set; }

    }
}
