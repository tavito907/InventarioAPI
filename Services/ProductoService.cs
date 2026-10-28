using AutoMapper;
using InventarioAPI.Data;
using InventarioAPI.DTOs.Compartidos;
using InventarioAPI.DTOs.Productos;
using InventarioAPI.Enums;
using InventarioAPI.Exceptions;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using InventarioAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InventarioAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        private readonly ILogger<ProductoService> _logger;

        private readonly IMapper _mapper;

        public ProductoService(IProductoRepository productoRepository, ILogger<ProductoService> logger, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginacionDto<ProductoDto>> ObtenerTodosAsync(BuscarProductoDto filtros)
        {

            _logger.LogInformation("Buscando productos. Nombre: {Nombre}, Categoria: {CategoriaId}",
                filtros.Nombre,
                filtros.CategoriaId);

            return await _productoRepository.ObtenerTodosAsync(filtros);

        }
        public async Task<ProductoDto> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);

            if(producto is null)
            {
                throw new NotFoundException("El producto no existe");
            }

            return producto;
        }

        public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
        {
           var categoria = await _productoRepository.ObtenerCategoriaAsync(dto.CategoriaId);

            if(categoria is null)
            {
                throw new NotFoundException("La categoria no existe.");
            }

            var producto = _mapper.Map<Producto>(dto);

            await _productoRepository.AgregarAsync(producto);
            await _productoRepository.GuardarCambiosAsync();

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Categoria = categoria.Nombre,
            };

        }

        public async Task ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await _productoRepository.ObtenerEntidadPorIdAsync(id);

            if (producto is null)
            {
                throw new NotFoundException("Producto no encontrado");
            }

            var categoria = await _productoRepository.ObtenerCategoriaAsync(dto.CategoriaId);

            if(categoria is null)
            {
                throw new NotFoundException("La categoria no existe.");
            }

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;

            await _productoRepository.ActualizarAsync(producto);
            await _productoRepository.GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _productoRepository.ObtenerEntidadPorIdAsync(id);

            if (producto is null)
            {
                throw new NotFoundException("El producto no existe.");
            }

            await _productoRepository.EliminarAsync(producto);

            await _productoRepository.GuardarCambiosAsync();
        }
    }
}
