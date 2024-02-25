﻿// <auto-generated />
using System;
using GraphQLDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GraphQLDemo.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240220184124_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.1.24081.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GraphQLDemo.Service.Models.Human", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Humans");
                });

            modelBuilder.Entity("GraphQLDemo.Service.Models.Trait", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Consequence")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Traits");
                });

            modelBuilder.Entity("HumanTraits", b =>
                {
                    b.Property<Guid>("IconId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TraitId")
                        .HasColumnType("uuid");

                    b.HasKey("IconId", "TraitId");

                    b.HasIndex("TraitId");

                    b.ToTable("HumanTraits");
                });

            modelBuilder.Entity("HumanTraits", b =>
                {
                    b.HasOne("GraphQLDemo.Service.Models.Human", null)
                        .WithMany()
                        .HasForeignKey("IconId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQLDemo.Service.Models.Trait", null)
                        .WithMany()
                        .HasForeignKey("TraitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
