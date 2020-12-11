﻿// <auto-generated />
using System;
using Meaurse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Meaurse.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201208192731_AddMeaurseDB")]
    partial class AddMeaurseDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Meaurse.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<float>("humidity")
                        .HasColumnType("real");

                    b.Property<float>("temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Meaurses");
                });
#pragma warning restore 612, 618
        }
    }
}
