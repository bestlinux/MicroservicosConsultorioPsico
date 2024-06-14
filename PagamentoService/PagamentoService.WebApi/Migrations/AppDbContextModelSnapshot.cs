﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PagamentoService.Persistence.Context;

#nullable disable

namespace PagamentoService.WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PagamentoService.Domain.Entities.Pagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<int?>("Mes")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int>("StatusPagamento")
                        .HasColumnType("int");

                    b.Property<double?>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Pagamentos");
                });
#pragma warning restore 612, 618
        }
    }
}