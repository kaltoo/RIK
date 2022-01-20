﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApp.DAL;

#nullable disable

namespace WebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApp.Domain.Isik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Kood")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Nimi1")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.Property<string>("Nimi2")
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.Property<int>("Tyyp")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Isikud");
                });

            modelBuilder.Entity("WebApp.Domain.Osalemine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IsikId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("KustutamiseKP")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Lisainfo")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<int>("Makseviis")
                        .HasColumnType("integer");

                    b.Property<int>("OsalejateArv")
                        .HasColumnType("integer")
                        .HasComment("Üritusele saabuvate osalejate arv. Füüsilise isiku puhul alati 1. Juriidilise isiku puhul 1 - 1000000.");

                    b.Property<int>("YritusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IsikId");

                    b.HasIndex("YritusId");

                    b.ToTable("Osalemised");
                });

            modelBuilder.Entity("WebApp.Domain.Yritus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Algus")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Info")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Koht")
                        .HasMaxLength(125)
                        .HasColumnType("character varying(125)");

                    b.Property<DateTime?>("KustutamiseKP")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nimi")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("Id");

                    b.ToTable("Yritused");
                });

            modelBuilder.Entity("WebApp.Domain.Osalemine", b =>
                {
                    b.HasOne("WebApp.Domain.Isik", "Isik")
                        .WithMany()
                        .HasForeignKey("IsikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApp.Domain.Yritus", "Yritus")
                        .WithMany()
                        .HasForeignKey("YritusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Isik");

                    b.Navigation("Yritus");
                });
#pragma warning restore 612, 618
        }
    }
}