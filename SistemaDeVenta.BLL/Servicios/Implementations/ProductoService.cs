using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Producto;
using SistemaDeVenta.Models.Entities;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class ProductoService : IProductoService
    {

        private readonly IMapper _mapper;
        private readonly IGenericRepository<Producto> _ProductoRepositorio;

        public ProductoService(IMapper mapper, IGenericRepository<Producto> productoRepositorio)
        {
            _mapper = mapper;
            _ProductoRepositorio = productoRepositorio;
        }
        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _ProductoRepositorio.Consultar();
                var listaProductos = queryProducto.Include(p => p.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProductos);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {

                var productoCreado = await _ProductoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ProductoDTO>(productoCreado);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _ProductoRepositorio.Obtener(u => u.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("No se encontro el producto");

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _ProductoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el producto");


                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _ProductoRepositorio.Obtener(p => p.IdProducto == id);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("No se encontro el producto");

                bool resultado = await _ProductoRepositorio.Eliminar(productoEncontrado);

                if (!resultado)
                    throw new TaskCanceledException("No se pudo eliminar");

                return resultado;

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
