using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Servicos;
using Subasta.Infra.Data;
using Subasta.Infra.Repositorios;

namespace Subasta.Api
{
    public static class Bootstrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //infra
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, ContextoSubasta>();
            services.AddScoped<IRepositorioItem, RepositorioItem>();
            services.AddScoped<IRepositorioLance, RepositorioLance>();
            services.AddScoped<IRepositorioLeilao, RepositorioLeilao>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

            //domain
            services.AddScoped<IServicoLeilao, ServicoLeilao>();
            services.AddScoped<IServicoUsuario, ServicoUsuario>();
            services.AddScoped<IServicoItem, ServicoItem>();
        }
    }
}
