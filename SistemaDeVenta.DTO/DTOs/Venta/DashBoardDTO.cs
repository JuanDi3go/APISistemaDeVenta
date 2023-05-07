namespace SistemaDeVenta.DTO.DTOs.Venta
{
    public class DashBoardDTO
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public List<VentaSemanaDTO> VentasUltimaSemana { get; set; }
    }
}
