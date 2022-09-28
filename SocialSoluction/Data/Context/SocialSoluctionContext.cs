using Microsoft.EntityFrameworkCore;
using SocialSoluction.Entities;
using SocialSoluction.Entities.Config;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace SocialSoluction.Context
{
    public class SocialSoluctionContext : DbContext
    {
        public SocialSoluctionContext(DbContextOptions<SocialSoluctionContext> options)
            : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ClienteConfig().Configure(modelBuilder.Entity<Cliente>());
            new AnuncioConfig().Configure(modelBuilder.Entity<Anuncio>());
        }


    }
}
