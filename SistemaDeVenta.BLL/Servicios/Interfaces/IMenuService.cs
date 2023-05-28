using SistemaDeVenta.DTO.DTOs.Menu;

namespace SistemaDeVenta.BLL.Servicios.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetMenuList(int idUsuario);
    }
}
