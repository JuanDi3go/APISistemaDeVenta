using SistemaDeVenta.Models.Entities;

namespace SistemaDeVenta.DAL.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
