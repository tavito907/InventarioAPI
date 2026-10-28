namespace InventarioAPI.Interfaces
{
    public interface IDescuentoStrategy
    {
        decimal CalcularDescuento(decimal subtotal);
    }
}
