using Microsoft.AspNetCore.Mvc;
using SistemaDeVenta.API.Utilities;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DTO.DTOs.Venta;

namespace SistemaDeVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardService;

        public DashBoardController(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new GenericReponse<DashBoardDTO>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _dashboardService.Resumen();
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
