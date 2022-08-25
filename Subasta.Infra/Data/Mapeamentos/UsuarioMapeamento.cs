using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subasta.Dominio.Entidades;

namespace Subasta.Infra.Data.Mapeamentos
{
    public class UsuarioMapeamento : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Login).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DataNascimento).IsRequired();
        }
    }
}
