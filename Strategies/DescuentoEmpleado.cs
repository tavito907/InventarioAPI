using InventarioAPI.Interfaces;

namespace InventarioAPI.Strategies
{
    public class DescuentoEmpleado : IDescuentoStrategy
    {
        public decimal CalcularDescuento(decimal subtotal)
        {
            return subtotal * 0.20m;
        }
    }
}
