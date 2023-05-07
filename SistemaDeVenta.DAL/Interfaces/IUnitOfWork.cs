namespace SistemaDeVenta.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
