using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaDeVenta.DAL.Context;
using SistemaDeVenta.DAL.Interfaces;
using SistemaDeVenta.DAL.Repositories;

namespace SistemaDeVenta.IOC
{
    public static class Dependency
    {
        public static void AddInversionOfControlLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(op => op.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

        }
    }
}
