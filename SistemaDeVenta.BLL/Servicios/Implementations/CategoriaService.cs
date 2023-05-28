using AutoMapper;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Categoria;
using SistemaDeVenta.Models.Entities;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepositorio;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try
            {
                var listaCategoria = await _categoriaRepositorio.Consultar();

                return _mapper.Map<List<CategoriaDTO>>(listaCategoria.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
