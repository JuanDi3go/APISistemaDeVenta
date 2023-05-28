using SistemaDeVenta.DTO.DTOs.Rol;

namespace SistemaDeVenta.BLL.Servicios.Interfaces
{
    public interface IRolService
    {
        Task<List<RolDTO>> Lista();
    }
}
