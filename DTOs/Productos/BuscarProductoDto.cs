using System.ComponentModel.DataAnnotations;
using InventarioAPI.Enums;

namespace InventarioAPI.DTOs.Productos
{
    /// <summary>
    /// Contiene los filtros opcionales para realizar la busqueda del producto.
    /// </summary>
    public class BuscarProductoDto
    {
        /// <summary>
        /// Nombre o parte del nombre del producto que se desea buscar.
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Precio minimo que debe tener el producto para ser incluido en los resultados.
        /// </summary>
        public decimal? PrecioMin {  get; set; }

        /// <summary>
        /// Precio maximo que debe tener el producto para ser incluido en los resultados.
        /// </summary>
        public decimal? PrecioMax { get; set; }

        /// <summary>
        /// Identificador de la categoria a la que debe pertenencer el producto.
        /// </summary>
        public int? CategoriaId { get; set; }

        /// <summary>
        /// Campo por el cual se ordenarán los resultados de la busqueda.
        /// </summary>
        public OrdenProducto? OrdenarPor {  get; set; }

        /// <summary>
        /// Indica si el ordenamiento sera descendente. Si es false, el orden será ascendente.
        /// </summary>
        public bool Descendente { get; set; } = false;

        /// <summary>
        /// Numero de página que se debe consultar. La primera página es la 1 y debe ser mayor que cero.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Cantidad máxima de productos que se devolveran por pagina. Debe estar entre 1 y 100.
        /// </summary>
        [Range(1, 100)]
        public int TamanoPagina { get; set; } = 10;
    }
}
