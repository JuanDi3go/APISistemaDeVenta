
using Microsoft.EntityFrameworkCore;
using SistemaDeVenta.DAL.Context;
using SistemaDeVenta.DAL.Interfaces;
using System.Linq.Expressions;

namespace SistemaDeVenta.DAL.Repositories
{
    public class GenericRepository<T> : IUnitOfWork, IGenericRepository<T> where T : class
    {

        private readonly DbventaContext _context;

        public GenericRepository(DbventaContext context)
        {
            _context = context;
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                var model = await _context.Set<T>().FirstOrDefaultAsync(filtro);

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null)
        {
            try
            {
                IQueryable<T> queryModelo = filtro == null ? _context.Set<T>() : _context.Set<T>().Where(filtro);


                return queryModelo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> Crear(T modelo)
        {
            try
            {
                await _context.Set<T>().AddAsync(modelo);

                return modelo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(T modelo)
        {
            _context.Set<T>().Update(modelo);

            return true;
        }

        public async Task<bool> Eliminar(T modelo)
        {
            _context.Set<T>().Remove(modelo);

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
