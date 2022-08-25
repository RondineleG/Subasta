using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subasta.Dominio.Entidades;

namespace Subasta.Infra.Data.Mapeamentos
{
    public class LeilaoMapeamento : IEntityTypeConfiguration<Leilao>
    {
        public void Configure(EntityTypeBuilder<Leilao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataInicio).IsRequired();
            builder.Property(x => x.DataFinal).IsRequired(false);
            builder.HasMany(x => x.Itens).WithOne(i => i.Leilao);
        }
    }
}
