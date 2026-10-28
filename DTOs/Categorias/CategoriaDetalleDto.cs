namespace InventarioAPI.DTOs.Categorias
{
    public class CategoriaDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ProductoCategoriaDto> Productos { get; set; } = new();
    }
}
