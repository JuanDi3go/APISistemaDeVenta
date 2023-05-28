using SistemaDeVenta.DTO.DTOs.Venta;

namespace SistemaDeVenta.BLL.Servicios.Interfaces
{
    public interface IVentaService
    {
        Task<VentaDTO> Registrar(VentaDTO modelo);
        Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);

        Task<List<ReporteDTO>> Reporte(string fechainicio, string fechaFin);


    }
}
