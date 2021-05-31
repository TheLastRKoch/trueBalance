﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using TRueBalance.Data;
using System;
using TRueBalance.Data.Entities;

namespace TRueBalance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180826093746_Add_orders_times")]
    partial class Add_orders_times
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

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

            modelBuilder.Entity("PlaceMT.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("placeMT.Data.Entities.Meal", b =>
                {
                    b.Property<int>("MealID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.HasKey("MealID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("placeMT.Data.Entities.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientName");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Observation");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("State");

                    b.HasKey("OrderID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("placeMT.Data.Entities.OrderMeal", b =>
                {
                    b.Property<int>("OrderMealID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MealID");

                    b.Property<int?>("OrderID");

                    b.HasKey("OrderMealID");

                    b.HasIndex("MealID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderMeals");
                });

            modelBuilder.Entity("PlaceMT.Data.Entities.PrintQueue", b =>
                {
                    b.Property<int>("PrintElementID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LinkedInvoiceInvoiceID");

                    b.Property<string>("Vendor");

                    b.HasKey("PrintElementID");

                    b.HasIndex("LinkedInvoiceInvoiceID");

                    b.ToTable("PrintQueues");
                });

            modelBuilder.Entity("placeMT.Data.Entities.UserSetting", b =>
                {
                    b.Property<int>("SettingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Key");

                    b.Property<string>("User");

                    b.Property<string>("Value");

                    b.HasKey("SettingID");

                    b.HasIndex("CategoryID");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("placeMT.DB.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cash");

                    b.Property<string>("ClientName");

                    b.Property<DateTime>("Date")
                        .HasMaxLength(10);

                    b.Property<string>("PaymentType");

                    b.Property<string>("State");

                    b.Property<int>("TotalToPay");

                    b.HasKey("InvoiceID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("placeMT.DB.Entities.Products", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("DueDate")
                        .HasMaxLength(10);

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("placeMT.DB.Entities.Sell", b =>
                {
                    b.Property<int>("SellID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("InvoiceID");

                    b.Property<int?>("MealID");

                    b.Property<string>("Salesman");

                    b.HasKey("SellID");

                    b.HasIndex("InvoiceID");

                    b.HasIndex("MealID");

                    b.ToTable("Sells");
                });

            modelBuilder.Entity("PlaceMT.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ImgURl")
                        .HasMaxLength(500);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

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
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TRueBalance.Data.Entities.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.HasKey("NotificationID");

                    b.ToTable("Notifications");
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
                    b.HasOne("PlaceMT.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PlaceMT.Models.ApplicationUser")
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

                    b.HasOne("PlaceMT.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PlaceMT.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("placeMT.Data.Entities.Meal", b =>
                {
                    b.HasOne("PlaceMT.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("placeMT.Data.Entities.OrderMeal", b =>
                {
                    b.HasOne("placeMT.Data.Entities.Meal", "Meal")
                        .WithMany("OrderMeals")
                        .HasForeignKey("MealID");

                    b.HasOne("placeMT.Data.Entities.Order", "Order")
                        .WithMany("OrderMeals")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("PlaceMT.Data.Entities.PrintQueue", b =>
                {
                    b.HasOne("placeMT.DB.Entities.Invoice", "LinkedInvoice")
                        .WithMany()
                        .HasForeignKey("LinkedInvoiceInvoiceID");
                });

            modelBuilder.Entity("placeMT.Data.Entities.UserSetting", b =>
                {
                    b.HasOne("PlaceMT.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("placeMT.DB.Entities.Sell", b =>
                {
                    b.HasOne("placeMT.DB.Entities.Invoice")
                        .WithMany("LinkedSells")
                        .HasForeignKey("InvoiceID");

                    b.HasOne("placeMT.Data.Entities.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealID");
                });
#pragma warning restore 612, 618
        }
    }
}
