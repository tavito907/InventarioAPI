using InventarioAPI.Enums;
using InventarioAPI.Interfaces;
using InventarioAPI.Strategies;

namespace InventarioAPI.Factories
{
    public class DescuentoFactory
    {
        public IDescuentoStrategy Crear(TipoCliente tipoCliente)
        {
            return tipoCliente switch
            {
                TipoCliente.Normal => new DescuentoNormal(),
                TipoCliente.Frecuente => new DescuentoFrecuente(),
                TipoCliente.VIP => new DescuentoVIP(),
                TipoCliente.Empleado => new DescuentoEmpleado(),
                _ => throw new ArgumentOutOfRangeException(nameof(tipoCliente))
            };
        }
    }
}
