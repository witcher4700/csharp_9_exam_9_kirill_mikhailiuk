﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebMoney.Models;

namespace WebMoney.Migrations
{
    [DbContext(typeof(WebMoneyContext))]
    [Migration("20211030111725_Companies")]
    partial class Companies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("WebMoney.Models.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("MoneyCount")
                        .HasColumnType("double precision");

                    b.Property<string>("UniqueNumber")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BankAccounts");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            MoneyCount = 0.0,
                            UniqueNumber = "MEGACOM11111",
                            UserId = "1"
                        },
                        new
                        {
                            Id = 3,
                            MoneyCount = 0.0,
                            UniqueNumber = "AKNET2222222",
                            UserId = "2"
                        },
                        new
                        {
                            Id = 4,
                            MoneyCount = 0.0,
                            UniqueNumber = "SVET33333333",
                            UserId = "3"
                        },
                        new
                        {
                            Id = 5,
                            MoneyCount = 0.0,
                            UniqueNumber = "GAS444444444",
                            UserId = "4"
                        },
                        new
                        {
                            Id = 6,
                            MoneyCount = 0.0,
                            UniqueNumber = "O!5555555555",
                            UserId = "5"
                        });
                });

            modelBuilder.Entity("WebMoney.Models.CompanyUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Walletnumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CompanyUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            PhoneNumber = "996551222770"
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 2,
                            PhoneNumber = "996551222770"
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 3,
                            PhoneNumber = "996551222770"
                        },
                        new
                        {
                            Id = 4,
                            CompanyId = 4,
                            PhoneNumber = "996551222770"
                        },
                        new
                        {
                            Id = 5,
                            CompanyId = 5,
                            PhoneNumber = "996551222770"
                        });
                });

            modelBuilder.Entity("WebMoney.Models.ServiceCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompanyType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ServiceCompanies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyType = 1,
                            Name = "Megacom"
                        },
                        new
                        {
                            Id = 2,
                            CompanyType = 2,
                            Name = "Aknet"
                        },
                        new
                        {
                            Id = 3,
                            CompanyType = 0,
                            Name = "Svet"
                        },
                        new
                        {
                            Id = 4,
                            CompanyType = 0,
                            Name = "Gas"
                        },
                        new
                        {
                            Id = 5,
                            CompanyType = 1,
                            Name = "O!"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
