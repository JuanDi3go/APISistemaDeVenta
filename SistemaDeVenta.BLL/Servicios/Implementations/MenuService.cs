using AutoMapper;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Menu;
using SistemaDeVenta.Models.Entities;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Usuario> _UsuarioRepository;
        private readonly IGenericRepository<MenuRol> _MenuRolRepository;
        private readonly IGenericRepository<Menu> _MenuRepository;

        public MenuService(IMapper mapper, IGenericRepository<Usuario> usuarioRepository, IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository)
        {
            _mapper = mapper;
            _UsuarioRepository = usuarioRepository;
            _MenuRolRepository = menuRolRepository;
            _MenuRepository = menuRepository;
        }

        public async Task<List<MenuDTO>> GetMenuList(int idUsuario)
        {
            IQueryable<Usuario> tblUsuario = await _UsuarioRepository.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tblMenuRol = await _MenuRolRepository.Consultar();
            IQueryable<Menu> tblMenu = await _MenuRepository.Consultar();

            try
            {
                IQueryable<Menu> tblResultado = (from u in tblUsuario
                                                 join mr in tblMenuRol on u.IdRol equals mr.IdRol
                                                 join m in tblMenu on mr.IdMenu equals m.IdMenu
                                                 select m).AsQueryable();

                var listaMenu = tblResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenu);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
