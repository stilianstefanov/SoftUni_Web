﻿// <auto-generated />
using System;
using Forum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Forum.Data.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20230602092700_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Forum.Data.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eb765af1-79d6-4c73-a9e4-ddef2bfbe4f1"),
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed venenatis libero vel nibh ultricies mattis. Sed sagittis sem in leo.",
                            Title = "My first post"
                        },
                        new
                        {
                            Id = new Guid("edb8b8d7-9bb8-4fc4-9845-bd1d134267a6"),
                            Content = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...",
                            Title = "My second post"
                        },
                        new
                        {
                            Id = new Guid("aad1921c-4c2d-4876-a228-4d82843a76e5"),
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vel pretium velit, eget imperdiet massa. In diam dolor, hendrerit. ",
                            Title = "My third post"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}