﻿// <auto-generated />
using System;
using CRUDProductCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRUDProductCatalog.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240627061859_CrearRelacionTresTablas")]
    partial class CrearRelacionTresTablas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRUDProductCatalog.Entities.Expediente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnostic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DiagnosticDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Expedientes");
                });

            modelBuilder.Entity("CRUDProductCatalog.Entities.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SpecialistId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExpedienteId");

                    b.HasIndex("SpecialistId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("CRUDProductCatalog.Entities.Specialist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Major")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialists");
                });

            modelBuilder.Entity("CRUDProductCatalog.Entities.Patient", b =>
                {
                    b.HasOne("CRUDProductCatalog.Entities.Expediente", "Expediente")
                        .WithMany("Patients")
                        .HasForeignKey("ExpedienteId");

                    b.HasOne("CRUDProductCatalog.Entities.Specialist", "Specialist")
                        .WithMany("Patients")
                        .HasForeignKey("SpecialistId");

                    b.Navigation("Expediente");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("CRUDProductCatalog.Entities.Expediente", b =>
                {
                    b.Navigation("Patients");
                });

            modelBuilder.Entity("CRUDProductCatalog.Entities.Specialist", b =>
                {
                    b.Navigation("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}