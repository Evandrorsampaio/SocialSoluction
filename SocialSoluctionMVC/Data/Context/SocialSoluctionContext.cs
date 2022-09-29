using Microsoft.EntityFrameworkCore;
using SocialSoluctionMVC.Entities;
using SocialSoluctionMVC.Entities.Config;
using SocialSoluctionMVC.Entities.Interfaces;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
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

        public override int SaveChanges()
        {
            ApplySoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ApplySoftDelete();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplySoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entry.Entity.GetType()))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["Excluido"] = false;
                            entry.CurrentValues["Exclusao"] = null;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["Excluido"] = true;
                            entry.CurrentValues["Exclusao"] = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
