using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaDeVenta.BLL.Servicios.Implementations;
using SistemaDeVenta.BLL.Servicios.Interfaces;
using SistemaDeVenta.DAL.Context;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DAL.Repositories;
using SistemaDeVenta.Utility;

namespace SistemaDeVenta.IOC
{
    public static class Dependency
    {
        public static void AddInversionOfControlLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(op => op.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            #region Automapper

            services.AddAutoMapper(typeof(AutomapperProfile));

            #endregion

            #region Services

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();


            #endregion


        }
    }
}
