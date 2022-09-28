using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialSoluction.Entities.Config
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Anuncios)
                .WithOne(c => c.Cliente);
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.Nome).IsRequired();
            builder.Property(c => c.CPFCNPJ).IsRequired();
        }
    }
}
