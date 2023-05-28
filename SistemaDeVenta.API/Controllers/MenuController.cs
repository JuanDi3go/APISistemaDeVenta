using Microsoft.AspNetCore.Mvc;
using SistemaDeVenta.API.Utilities;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DTO.DTOs.Menu;

namespace SistemaDeVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new GenericReponse<List<MenuDTO>>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _menuService.GetMenuList(idUsuario);
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
