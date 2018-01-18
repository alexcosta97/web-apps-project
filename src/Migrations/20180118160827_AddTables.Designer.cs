﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using src.Data;
using src.Models;
using System;

namespace src.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180118160827_AddTables")]
    partial class AddTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("src.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<string>("county");

                    b.Property<string>("postCode");

                    b.Property<string>("street1");

                    b.Property<string>("street2");

                    b.HasKey("AddressID");

                    b.HasIndex("ApplicationUserID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("src.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("src.Models.Favourite", b =>
                {
                    b.Property<int>("FavouriteID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("LineID");

                    b.Property<string>("Name");

                    b.Property<int?>("RouteID");

                    b.HasKey("FavouriteID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("LineID");

                    b.HasIndex("RouteID");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("src.Models.Line", b =>
                {
                    b.Property<int>("LineID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Start");

                    b.HasKey("LineID");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("src.Models.Route", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Arrival");

                    b.Property<DateTime>("Departure");

                    b.Property<int>("Direction");

                    b.Property<string>("DriverID");

                    b.Property<int?>("DriverStaffID");

                    b.Property<int>("LineID");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.HasKey("RouteID");

                    b.HasIndex("DriverStaffID");

                    b.HasIndex("LineID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("src.Models.RouteStop", b =>
                {
                    b.Property<int>("RouteStopID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RouteID");

                    b.Property<int>("StopID");

                    b.HasKey("RouteStopID");

                    b.HasIndex("RouteID");

                    b.HasIndex("StopID");

                    b.ToTable("RouteStops");
                });

            modelBuilder.Entity("src.Models.Staff", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<string>("accountNumber");

                    b.Property<int>("hoursContracted");

                    b.Property<string>("nationalInsuranceNumber");

                    b.Property<string>("sortCode");

                    b.HasKey("StaffID");

                    b.HasIndex("ApplicationUserID");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("src.Models.Stop", b =>
                {
                    b.Property<int>("StopID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int?>("RouteID");

                    b.HasKey("StopID");

                    b.HasIndex("RouteID");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("src.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("src.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("src.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("src.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("src.Models.Address", b =>
                {
                    b.HasOne("src.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Addresses")
                        .HasForeignKey("ApplicationUserID");
                });

            modelBuilder.Entity("src.Models.Favourite", b =>
                {
                    b.HasOne("src.Models.ApplicationUser")
                        .WithMany("Favourites")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("src.Models.Line", "Line")
                        .WithMany()
                        .HasForeignKey("LineID");

                    b.HasOne("src.Models.Route", "route")
                        .WithMany()
                        .HasForeignKey("RouteID");
                });

            modelBuilder.Entity("src.Models.Route", b =>
                {
                    b.HasOne("src.Models.Staff", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverStaffID");

                    b.HasOne("src.Models.Line")
                        .WithMany("Routes")
                        .HasForeignKey("LineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("src.Models.RouteStop", b =>
                {
                    b.HasOne("src.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("src.Models.Stop", "Stop")
                        .WithMany()
                        .HasForeignKey("StopID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("src.Models.Staff", b =>
                {
                    b.HasOne("src.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");
                });

            modelBuilder.Entity("src.Models.Stop", b =>
                {
                    b.HasOne("src.Models.Route")
                        .WithMany("Stops")
                        .HasForeignKey("RouteID");
                });
#pragma warning restore 612, 618
        }
    }
}
