﻿// <auto-generated />
using System;
using MicroShop.ProductAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MicroShop.ProductAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240623192252_ProductSeed2")]
    partial class ProductSeed2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MicroShop.ProductAPI.Domain.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = "01J138T7V7HSBWDN1D0S6NB5PV",
                            Name = "Metade de um par de meias",
                            Price = 5.9m,
                            Stock = 99
                        },
                        new
                        {
                            Id = "01J138T7V73QA71J1D6SVGFHTA",
                            Name = "Camiseta",
                            Price = 15.9m,
                            Stock = 23
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
