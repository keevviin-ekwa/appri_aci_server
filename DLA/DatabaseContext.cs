using ApproACI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Reflection.Emit;

namespace ApproACI.DLA
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Produit> Produits { get; set; }

        public DbSet<Matiere> Matieres { get; set; }

        public DbSet<Stock> Stocks { get; set; }


        public DbSet<Marque> Marques { get; set; }

        public DbSet<Objectif> Objectif { get; set; }
        public DbSet<MatiereProduit> MatiereProduits { get; set; }

        public virtual DbSet<DroitsAcces> DroitAcces { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }

        public virtual DbSet<Livraison> Livraisons { get; set; }



        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
       
          }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
            
          builder.Entity<MatiereProduit>().HasKey(t => new { t.MatiereId, t.ProduitId });

          builder.Entity<MatiereProduit>()
                        .HasOne(t => t.Produit)
                       .WithMany(t => t.MatiereProduits)
                     .HasForeignKey(t => t.ProduitId);


            builder.Entity<MatiereProduit>()
                      .HasOne(t => t.Matiere)
                      .WithMany(t => t.MatiereProduits)
                       .HasForeignKey(t => t.MatiereId);   

                
        }


    }
}
