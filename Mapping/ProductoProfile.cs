using AutoMapper;
using InventarioAPI.DTOs.Productos;
using InventarioAPI.Models;

namespace InventarioAPI.Mapping
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<CrearProductoDto, Producto>();

            CreateMap<Producto, ProductoDto>()
                .ForMember(
                    destino => destino.Categoria,
                    opt => opt.MapFrom(origen => origen.Categoria.Nombre)
                );
        }
    }
}
