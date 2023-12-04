﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vezeeta.Data;

#nullable disable

namespace Vezeeta.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.HasIndex("SpecializationId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Identity.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Doctor",
                            NormalizedName = "DOCTOR"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Patient",
                            NormalizedName = "PATIENT"
                        });
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ba40c8d2-5bff-4711-9559-ee5a4e3f0ba8",
                            DateOfBirth = new DateTime(1980, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@vezeeta.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            Gender = 1,
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@VEZEETA.COM",
                            NormalizedUserName = "ADMIN@VEZEETA.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOjroekVwwqWUY5R0JggUWcCORYXrTfI+FXd0BorWyPKrER3GLeE4KYcMyrH4YuYCw==",
                            PhoneNumber = "1234567890",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "admin@vezeeta.com",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specializations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Allergy and Immunology"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Anesthesiology"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Audiology"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cardiology"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Cardiothoracic Surgery"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Cardiology and Vascular Disease"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Chest and Respiratory"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Colon and Rectal Surgery"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Dentistry"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Dermatology"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Diabetes and Endocrinology"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Dietitian and Nutrition"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Emergency Medicine"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Ear, Nose and Throat"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Family Medicine"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Forensic Pathology"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Gastroenterology and Endoscopy"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Genetics and Genomics"
                        },
                        new
                        {
                            Id = 19,
                            Name = "General Practice"
                        },
                        new
                        {
                            Id = 20,
                            Name = "General Surgery"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Geriatrics"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Hematology"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Hepatology"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Hospital Medicine"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Hospice and Palliative Medicine"
                        },
                        new
                        {
                            Id = 26,
                            Name = "IVF and Infertility"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Internal Medicine"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Interventional Radiology"
                        },
                        new
                        {
                            Id = 29,
                            Name = "Laboratories"
                        },
                        new
                        {
                            Id = 30,
                            Name = "Neurology"
                        },
                        new
                        {
                            Id = 31,
                            Name = "Neurosurgery"
                        },
                        new
                        {
                            Id = 32,
                            Name = "Obesity and Laparoscopic Surgery"
                        },
                        new
                        {
                            Id = 33,
                            Name = "Oncologic Surgery"
                        },
                        new
                        {
                            Id = 34,
                            Name = "Oncology"
                        },
                        new
                        {
                            Id = 35,
                            Name = "Ophthalmic Surgery"
                        },
                        new
                        {
                            Id = 36,
                            Name = "Ophthalmology"
                        },
                        new
                        {
                            Id = 37,
                            Name = "Orthopedic Surgery"
                        },
                        new
                        {
                            Id = 38,
                            Name = "Osteopathy"
                        },
                        new
                        {
                            Id = 39,
                            Name = "Otolaryngology"
                        },
                        new
                        {
                            Id = 40,
                            Name = "Pain Management"
                        },
                        new
                        {
                            Id = 41,
                            Name = "Pathology"
                        },
                        new
                        {
                            Id = 42,
                            Name = "Pediatric Surgery"
                        },
                        new
                        {
                            Id = 43,
                            Name = "Pediatrics"
                        },
                        new
                        {
                            Id = 44,
                            Name = "Phoniatrics"
                        },
                        new
                        {
                            Id = 45,
                            Name = "Physical Medicine and Rehabilitation"
                        },
                        new
                        {
                            Id = 46,
                            Name = "Physiotherapy and Sports Injuries"
                        },
                        new
                        {
                            Id = 47,
                            Name = "Plastic Surgery"
                        },
                        new
                        {
                            Id = 48,
                            Name = "Preventive Medicine"
                        },
                        new
                        {
                            Id = 49,
                            Name = "Psychiatry"
                        },
                        new
                        {
                            Id = 50,
                            Name = "Radiology"
                        },
                        new
                        {
                            Id = 51,
                            Name = "Rheumatology"
                        },
                        new
                        {
                            Id = 52,
                            Name = "Sleep Medicine"
                        },
                        new
                        {
                            Id = 53,
                            Name = "Spinal Surgery"
                        },
                        new
                        {
                            Id = 54,
                            Name = "Thoracic Surgery"
                        },
                        new
                        {
                            Id = 55,
                            Name = "Urology"
                        },
                        new
                        {
                            Id = 56,
                            Name = "Vascular Surgery"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Identity.ApplicationUser", "ApplicationUser")
                        .WithOne()
                        .HasForeignKey("Vezeeta.Core.Models.Doctor", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Specialization");
                });
#pragma warning restore 612, 618
        }
    }
}
