using AutoMapper;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Rol;
using SistemaDeVenta.Models.Entities;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class RolService : IRolService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Rol> _RolRepositorio;

        public RolService(IGenericRepository<Rol> rolRepositorio, IMapper mapper)
        {
            _RolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _RolRepositorio.Consultar();

                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
