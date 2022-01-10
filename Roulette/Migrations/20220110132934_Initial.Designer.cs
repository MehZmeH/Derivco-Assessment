﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Roulette.Db;

#nullable disable

namespace Roulette.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20220110132934_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Roulette.models.Bet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BetAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BetNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BetType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PayoutAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RouletteTableID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SpinNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Bets", (string)null);
                });

            modelBuilder.Entity("Roulette.models.RouletteTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpinIteration")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("RouletteTables", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            SpinIteration = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}