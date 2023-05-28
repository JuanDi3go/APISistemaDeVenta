using SistemaDeVenta.DTO.DTOs.Categoria;

namespace SistemaDeVenta.BLL.Servicios.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Lista();
    }
}
