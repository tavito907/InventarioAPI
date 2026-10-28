using InventarioAPI.Interfaces;

namespace InventarioAPI.Strategies
{
    public class DescuentoVIP : IDescuentoStrategy
    {
        public decimal CalcularDescuento(decimal subtotal)
        {
            return subtotal * 0.15m;
        }
    }
}
