using InventarioAPI.Interfaces;

namespace InventarioAPI.Strategies
{
    public class DescuentoFrecuente : IDescuentoStrategy
    {
        public decimal CalcularDescuento(decimal subtotal)
        {
            return subtotal * 0.05m;
        }
    }
}
