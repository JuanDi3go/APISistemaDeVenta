using SistemaDeVenta.DTO.DTOs.Venta;

namespace SistemaDeVenta.BLL.Servicios.Interfaces
{
    public interface IDashBoardService
    {
        Task<DashBoardDTO> Resumen();
    }
}
