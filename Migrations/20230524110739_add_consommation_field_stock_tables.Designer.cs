﻿// <auto-generated />
using System;
using ApproACI.DLA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApproACI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230524110739_add_consommation_field_stock_tables")]
    partial class add_consommation_field_stock_tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApproACI.Models.Commande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateLivraison")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatiereId")
                        .HasColumnType("int");

                    b.Property<string>("Quantite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SemaineCommande")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MatiereId");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("ApproACI.Models.DroitsAcces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDerniereMaj")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesignationTechnique")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DroitAcces");
                });

            modelBuilder.Entity("ApproACI.Models.Marque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marques");
                });

            modelBuilder.Entity("ApproACI.Models.Matiere", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Colissage")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<int>("DelaisAppro")
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Origine")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("PrixUnitaire")
                        .HasColumnType("float");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Matieres");
                });

            modelBuilder.Entity("ApproACI.Models.MatiereProduit", b =>
                {
                    b.Property<int>("MatiereId")
                        .HasColumnType("int");

                    b.Property<int>("ProduitId")
                        .HasColumnType("int");

                    b.Property<double>("ContributionMatiereGF")
                        .HasColumnType("float");

                    b.Property<double>("contributionMatierePF")
                        .HasColumnType("float");

                    b.HasKey("MatiereId", "ProduitId");

                    b.HasIndex("ProduitId");

                    b.ToTable("MatiereProduits");
                });

            modelBuilder.Entity("ApproACI.Models.Objectif", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Mois")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ObjectifGF")
                        .HasColumnType("float");

                    b.Property<double>("ObjectifPF")
                        .HasColumnType("float");

                    b.Property<int>("ProduitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProduitId");

                    b.ToTable("Objectif");
                });

            modelBuilder.Entity("ApproACI.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("MarqueId")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MarqueId");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("ApproACI.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Consommation")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<int>("MatiereId")
                        .HasColumnType("int");

                    b.Property<string>("Mois")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StockDebut")
                        .HasColumnType("float");

                    b.Property<double>("StockFin")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MatiereId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("ApproACI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDerniereMaj")
                        .HasColumnType("datetime2");

                    b.Property<string>("DroitAcces")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fonction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFirstConnection")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MatiereProduit", b =>
                {
                    b.Property<int>("MatieresId")
                        .HasColumnType("int");

                    b.Property<int>("ProduitsId")
                        .HasColumnType("int");

                    b.HasKey("MatieresId", "ProduitsId");

                    b.HasIndex("ProduitsId");

                    b.ToTable("MatiereProduit");
                });

            modelBuilder.Entity("ApproACI.Models.Commande", b =>
                {
                    b.HasOne("ApproACI.Models.Matiere", "Matiere")
                        .WithMany()
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matiere");
                });

            modelBuilder.Entity("ApproACI.Models.MatiereProduit", b =>
                {
                    b.HasOne("ApproACI.Models.Matiere", "Matiere")
                        .WithMany("MatiereProduits")
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApproACI.Models.Produit", "Produit")
                        .WithMany("MatiereProduits")
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matiere");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("ApproACI.Models.Objectif", b =>
                {
                    b.HasOne("ApproACI.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("ApproACI.Models.Produit", b =>
                {
                    b.HasOne("ApproACI.Models.Marque", "Marque")
                        .WithMany()
                        .HasForeignKey("MarqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marque");
                });

            modelBuilder.Entity("ApproACI.Models.Stock", b =>
                {
                    b.HasOne("ApproACI.Models.Matiere", "Matiere")
                        .WithMany("Stocks")
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matiere");
                });

            modelBuilder.Entity("MatiereProduit", b =>
                {
                    b.HasOne("ApproACI.Models.Matiere", null)
                        .WithMany()
                        .HasForeignKey("MatieresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApproACI.Models.Produit", null)
                        .WithMany()
                        .HasForeignKey("ProduitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApproACI.Models.Matiere", b =>
                {
                    b.Navigation("MatiereProduits");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("ApproACI.Models.Produit", b =>
                {
                    b.Navigation("MatiereProduits");
                });
#pragma warning restore 612, 618
        }
    }
}
