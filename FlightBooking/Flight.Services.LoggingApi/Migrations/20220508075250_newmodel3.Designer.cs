﻿// <auto-generated />
using System;
using Flight.Services.LoggingApi.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Flight.Services.LoggingApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220508075250_newmodel3")]
    partial class newmodel3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Flight.Services.LoggingApi.Model.Logs", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("log")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("senderAPI")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("logs");
                });
#pragma warning restore 612, 618
        }
    }
}
