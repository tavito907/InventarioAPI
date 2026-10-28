using InventarioAPI.Interfaces;

namespace InventarioAPI.Services
{
    public class DescuentoService
    {
        private readonly IDescuentoStrategy _estrategia;

        public DescuentoService(IDescuentoStrategy estrategia)
        {
            _estrategia = estrategia;
        }

        public decimal CalcularDescuento(decimal subtotal)
        {
            return _estrategia.CalcularDescuento(subtotal);
        }
    }
}
