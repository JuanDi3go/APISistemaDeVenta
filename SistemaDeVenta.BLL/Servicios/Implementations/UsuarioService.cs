using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DTO.DTOs.Usuario;
using SistemaDeVenta.Models.Entities;

namespace SistemaDeVenta.BLL.Servicios.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar();
                var listausuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(listausuarios);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(v => v.Correo == correo && v.Clave == clave);

                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no Existe");

                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();


                return _mapper.Map<SesionDTO>(devolverUsuario);


            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));
                if (usuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }
                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();


                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);

                var usuarioEncontrado = await _usuarioRepositorio.Obtener(id => id.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado != null)
                    throw new TaskCanceledException("El usuario no existe");

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioEncontrado.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioEncontrado.EsActivo;

                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(user => user.IdUsuario == id);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("Usuario no encontrado");

                bool respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo Eliminar");



                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
