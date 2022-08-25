using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Subasta.Dominio.Entidades;
using Subasta.Infra.Data;
using System.Linq;

namespace Subasta.Teste.Integracao.Infra
{
    public class SubastaWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var contextOptions = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ContextoSubasta>));
                if (contextOptions != null)
                    services.Remove(contextOptions);

                services.AddDbContext<ContextoSubasta>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });

                var serviceProvider = services.BuildServiceProvider();
                
                using var scope = serviceProvider.CreateScope();
                var contexto = scope.ServiceProvider.GetRequiredService<ContextoSubasta>();
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();
            });
        }

        public void AdicionarEntidades(params EntidadeBase[] entidades)
        {
            using var scope = Server.Services.CreateScope();
            using var contexto = scope.ServiceProvider.GetRequiredService<ContextoSubasta>();
            contexto.AddRange(entidades);
            contexto.SaveChanges();
        }

        public ContextoSubasta GetContexto()
        {
            var scope = Server.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ContextoSubasta>();
        }
    }
}
