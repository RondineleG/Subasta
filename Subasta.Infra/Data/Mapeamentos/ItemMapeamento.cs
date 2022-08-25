using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subasta.Dominio.Entidades;

namespace Subasta.Infra.Data.Mapeamentos
{
    public class ItemMapeamento : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
                
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Descricao).IsRequired();

            builder.HasOne(x => x.Leilao).WithMany().HasForeignKey(x => x.LeilaoId);
            builder.HasOne(x => x.Comprador).WithMany().HasForeignKey(x => x.CompradorId).IsRequired(false);
        }
    }
}