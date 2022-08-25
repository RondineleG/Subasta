using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subasta.Dominio.Entidades;

namespace Subasta.Infra.Data.Mapeamentos
{
    public class LanceMapeamento : IEntityTypeConfiguration<Lance>
    {
        public void Configure(EntityTypeBuilder<Lance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Item)
                .WithMany(x => x.Lances)
                .HasForeignKey(x => x.ItemId);

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
