using Microsoft.EntityFrameworkCore;
using SocialSoluctionMVC.Entities;
using SocialSoluctionMVC.Entities.Config;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace SocialSoluctionMVC.Data.Context
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
