using System.Text.Json.Serialization;

namespace InventarioAPI.Models
{
    public class Producto
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [JsonIgnore]
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
            = new List<DetalleVenta>();

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

    }
}
