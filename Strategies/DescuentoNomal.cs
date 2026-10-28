using InventarioAPI.Interfaces;

namespace InventarioAPI.Strategies
{
    public class DescuentoNormal : IDescuentoStrategy
    {
        public decimal CalcularDescuento(decimal subtotal)
        {
            return 0;
        }
    }
}
