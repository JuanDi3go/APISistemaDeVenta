using Microsoft.AspNetCore.Mvc;
using SistemaDeVenta.API.Utilities;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DTO.DTOs.Usuario;

namespace SistemaDeVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [Route("ListaUsuarios")]
        public async Task<IActionResult> ListaUsuarios()
        {
            var rsp = new GenericReponse<List<UsuarioDTO>>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _usuarioService.Lista();
            }
            catch (Exception ex)
            {
                rsp.Succeed = false;
                rsp.Message = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            var rsp = new GenericReponse<SesionDTO>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);
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
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new GenericReponse<UsuarioDTO>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _usuarioService.Crear(usuario);
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
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new GenericReponse<bool>();

            try
            {
                rsp.Succeed = true;
                rsp.Data = await _usuarioService.Editar(usuario);
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
                rsp.Data = await _usuarioService.Eliminar(id);
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
