using Microsoft.AspNetCore.Mvc;
using SistemaDeVenta.API.Utilities;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DTO.DTOs.Venta;

namespace SistemaDeVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }




        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO venta)
        {
            var rsp = new GenericReponse<VentaDTO>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ventaService.Registrar(venta);
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var rsp = new GenericReponse<List<VentaDTO>>();
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaFin = fechaFin is null ? "" : fechaFin;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string fechaInicio, string fechaFin)
        {
            var rsp = new GenericReponse<List<ReporteDTO>>();


            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ventaService.Reporte(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }

    }
}
