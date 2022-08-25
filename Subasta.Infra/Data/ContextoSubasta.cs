using Microsoft.EntityFrameworkCore;
using Subasta.Dominio.Entidades;
using Subasta.Infra.Data.Mapeamentos;

namespace Subasta.Infra.Data
{
    public class ContextoSubasta : DbContext
    {
        public DbSet<Item> Itens { get; set; }
        public DbSet<Lance> Lances { get; set; }
        public DbSet<Leilao> Leiloes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ContextoSubasta()
        {
        }

        public ContextoSubasta(DbContextOptions<ContextoSubasta> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemMapeamento());
            modelBuilder.ApplyConfiguration(new LanceMapeamento());
            modelBuilder.ApplyConfiguration(new LeilaoMapeamento());
            modelBuilder.ApplyConfiguration(new UsuarioMapeamento());
        }
    }
}
