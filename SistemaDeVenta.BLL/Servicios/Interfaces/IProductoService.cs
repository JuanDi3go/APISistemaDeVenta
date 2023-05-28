using SistemaDeVenta.DTO.DTOs.Producto;

namespace SistemaDeVenta.BLL.Servicios.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> Lista();
        Task<ProductoDTO> Crear(ProductoDTO modelo);
        Task<bool> Editar(ProductoDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
