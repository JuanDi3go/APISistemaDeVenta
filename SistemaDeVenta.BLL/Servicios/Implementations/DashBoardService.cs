using AutoMapper;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Venta;
using SistemaDeVenta.Models.Entities;
using System.Globalization;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IMapper _mapper;
        private readonly IVentaRepository _VentaRepositorio;
        private readonly IGenericRepository<Producto> _ProductoRepository;

        public DashBoardService(IMapper mapper, IVentaRepository ventaRepositorio, IGenericRepository<Producto> productoRepository)
        {
            _mapper = mapper;
            _VentaRepositorio = ventaRepositorio;
            _ProductoRepository = productoRepository;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmDashBoard = new DashBoardDTO();

            try
            {
                vmDashBoard.TotalVentas = await TotalVentasUltimaSemana();
                vmDashBoard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashBoard.TotalProductos = await TotalProductos();

                List<VentaSemanaDTO> listaVentasSemana = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentasSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }
                vmDashBoard.VentasUltimaSemana = listaVentasSemana;

                return vmDashBoard;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private IQueryable<Venta> RetornarVenta(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> query = await _VentaRepositorio.Consultar();
            if (query.Count() > 0)
            {
                var tablaVenta = RetornarVenta(query, -7);
                total = tablaVenta.Count();
            }
            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _VentaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, -7);

                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-CO"));

        }


        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _ProductoQuery = await _ProductoRepository.Consultar();
            int total = _ProductoQuery.Count();

            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            IQueryable<Venta> _ventaQuery = await _VentaRepositorio.Consultar();
            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, -7);

                resultado = tablaVenta.GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd-MM-yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;
        }


    }
}
