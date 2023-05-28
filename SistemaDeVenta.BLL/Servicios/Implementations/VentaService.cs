using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Venta;
using SistemaDeVenta.Models.Entities;
using System.Globalization;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class VentaService : IVentaService
    {
        private readonly IMapper _mapper;
        private readonly IVentaRepository _VentaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepository;

        public VentaService(IMapper mapper, IVentaRepository ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepository)
        {
            _mapper = mapper;
            _VentaRepositorio = ventaRepositorio;
            _detalleVentaRepository = detalleVentaRepository;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _VentaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No Se pudo crear");

                return _mapper.Map<VentaDTO>(ventaGenerada);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            try
            {
                IQueryable<Venta> query = await _VentaRepositorio.Consultar();
                var listaResultado = new List<Venta>();

                if (buscarPor == "fecha")
                {
                    DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));


                    listaResultado = await query.Where(v => v.FechaRegistro >= fechInicio && v.FechaRegistro <= fechFin)
                        .Include(dv => dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation).ToListAsync();

                }
                else
                {


                    listaResultado = await query.Where(v => v.NumeroDocumento == numeroVenta)
                        .Include(dv => dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }

                return _mapper.Map<List<VentaDTO>>(listaResultado);
            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<List<ReporteDTO>> Reporte(string fechainicio, string fechaFin)
        {
            try
            {
                IQueryable<DetalleVenta> query = await _detalleVentaRepository.Consultar();

                var listaResultado = new List<DetalleVenta>();
                DateTime fechInicio = DateTime.ParseExact(fechainicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));


                listaResultado = await query.Include(p => p.IdProductoNavigation).Include(v => v.IdVentaNavigation).Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fechInicio.Date &&
                dv.IdVentaNavigation.FechaRegistro.Value.Date <= fechFin.Date).ToListAsync();

                return _mapper.Map<List<ReporteDTO>>(listaResultado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
