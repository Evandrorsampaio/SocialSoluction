using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialSoluctionMVC.Entities.Config
{
    public class AnuncioConfig : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.Cliente)
                .WithMany(c => c.Anuncios)
                .HasForeignKey("ClienteId");
            builder.Property(a => a.ClienteId).IsRequired();
            builder.Property(a => a.Tipo).IsRequired();
            builder.Property(a => a.Descricao).IsRequired();
            builder.Property(a => a.Valor).IsRequired();
            builder.Property(a => a.Publicacao).IsRequired();
            builder.HasQueryFilter(p => !p.Excluido);

        }
    }
}
