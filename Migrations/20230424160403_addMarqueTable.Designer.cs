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
    [Migration("20230424160403_addMarqueTable")]
    partial class addMarqueTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<double>("ContributionMatiere")
                        .HasColumnType("float");

                    b.HasKey("MatiereId", "ProduitId");

                    b.HasIndex("ProduitId");

                    b.ToTable("MatiereProduits");
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

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("ApproACI.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<int>("MatiereId")
                        .HasColumnType("int");

                    b.Property<int>("Mois")
                        .HasColumnType("int");

                    b.Property<double>("StockDebut")
                        .HasColumnType("float");

                    b.Property<double>("StockFin")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MatiereId");

                    b.ToTable("Stocks");
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
