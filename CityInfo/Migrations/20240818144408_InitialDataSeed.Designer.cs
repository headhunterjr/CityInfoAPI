﻿// <auto-generated />
using CityInfo.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.Migrations
{
    [DbContext(typeof(CityDbContext))]
    [Migration("20240818144408_InitialDataSeed")]
    partial class InitialDataSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CityInfo.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "pizzas, hot dogs, busy streets",
                            Name = "NYC"
                        },
                        new
                        {
                            Id = 2,
                            Description = "buildings and that",
                            Name = "Lviv"
                        });
                });

            modelBuilder.Entity("CityInfo.Entities.Landmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Landmarks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "Times square",
                            Name = "NYC Square"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "tacooooooooooos",
                            Name = "NYC street food restaurant"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Description = "Market square",
                            Name = "Lviv square"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            Description = "best uni in the woooorrldddddd",
                            Name = "LNU"
                        });
                });

            modelBuilder.Entity("CityInfo.Entities.Landmark", b =>
                {
                    b.HasOne("CityInfo.Entities.City", "City")
                        .WithMany("Landmarks")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.Entities.City", b =>
                {
                    b.Navigation("Landmarks");
                });
#pragma warning restore 612, 618
        }
    }
}
