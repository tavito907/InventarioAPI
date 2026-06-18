using System.Text.Json.Serialization;

namespace InventarioAPI.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }

        [JsonIgnore]
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
            = new List<DetalleVenta>();
    }
}
