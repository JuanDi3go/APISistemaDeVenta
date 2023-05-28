using Microsoft.AspNetCore.Mvc;
using SistemaDeVenta.API.Utilities;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DTO.DTOs.Producto;

namespace SistemaDeVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _ProductoService;

        public ProductoController(IProductoService productoService)
        {
            _ProductoService = productoService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new GenericReponse<List<ProductoDTO>>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ProductoService.Lista();
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }



        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductoDTO producto)
        {
            var rsp = new GenericReponse<ProductoDTO>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ProductoService.Crear(producto);
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }



        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO producto)
        {
            var rsp = new GenericReponse<bool>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ProductoService.Editar(producto);
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Elimanar/{id:int}")]
        public async Task<IActionResult> Elimanar(int id)
        {
            var rsp = new GenericReponse<bool>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _ProductoService.Eliminar(id);
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
