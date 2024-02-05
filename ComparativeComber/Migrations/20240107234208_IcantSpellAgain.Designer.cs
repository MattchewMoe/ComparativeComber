﻿// <auto-generated />
using System;
using ComparativeComber.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComparativeComber.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240107234208_IcantSpellAgain")]
    partial class IcantSpellAgain
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ComparativeComber.Entities.ComparableSale", b =>
                {
                    b.Property<int>("ComparableSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComparableSaleId"));

                    b.Property<decimal?>("AnnualTaxes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("AssessedValueImp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("AssessedValueLand")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("AssessedValueTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BuildingSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildingSizeUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("BuildingSizeValue")
                        .HasColumnType("float");

                    b.Property<string>("CircumstancesOfSale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Configuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConstructionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfSale")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfSaleString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateVerifiedString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Financing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("FinancingAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FloodPlain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("FromHainesReport")
                        .HasColumnType("bit");

                    b.Property<bool?>("FromInternalReport")
                        .HasColumnType("bit");

                    b.Property<string>("Grantee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GranteeAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GranteePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GranteePrincipal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grantor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GrantorPrincipal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HainesReportSaleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighestAndBestUse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrls")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImprovementSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Indication")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndicationUnitType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("IndicationValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IntendedUse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LandToBuildingRatio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("NumberOfUnits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ORBookPage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginatingReportUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParcelNumbers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parking")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParkingRatio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonWhoVerifiedSale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresentUse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyRightsTransferred")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordingReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RenovationYears")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RentData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SalePriceString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolDistrict")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteSizeInAcreage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteSizeUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SiteSizeValue")
                        .HasColumnType("float");

                    b.Property<string>("SizeInSF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxingDistrict")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrafficCount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Utilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerifiersRelationshipToSale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("YearConstructed")
                        .HasColumnType("int");

                    b.Property<string>("YearConstructedString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zoning")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComparableSaleId");

                    b.ToTable("ComparableSales");
                });

            modelBuilder.Entity("ComparativeComber.Entities.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrganizationId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("OrganizationId");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            OrganizationId = 1,
                            CreatedAt = new DateTime(2024, 1, 7, 23, 42, 7, 812, DateTimeKind.Utc).AddTicks(9727),
                            Name = "Example Organization",
                            UpdatedAt = new DateTime(2024, 1, 7, 23, 42, 7, 812, DateTimeKind.Utc).AddTicks(9727)
                        });
                });

            modelBuilder.Entity("ComparativeComber.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ComparativeComber.Entities.User", b =>
                {
                    b.HasOne("ComparativeComber.Entities.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ComparativeComber.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ComparativeComber.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComparativeComber.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ComparativeComber.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComparativeComber.Entities.Organization", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
