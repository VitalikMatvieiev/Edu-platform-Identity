﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Identity_Infrastructure.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    partial class IdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClaimIdentity", b =>
                {
                    b.Property<int>("ClaimsId")
                        .HasColumnType("int");

                    b.Property<int>("IdentitiesId")
                        .HasColumnType("int");

                    b.HasKey("ClaimsId", "IdentitiesId");

                    b.HasIndex("IdentitiesId");

                    b.ToTable("ClaimIdentity");
                });

            modelBuilder.Entity("ClaimRole", b =>
                {
                    b.Property<int>("ClaimsId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("ClaimsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ClaimRole");
                });

            modelBuilder.Entity("Identity_Domain.Entities.Base.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Claim");
                });

            modelBuilder.Entity("Identity_Domain.Entities.Base.Identity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastLogout")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Identity");
                });

            modelBuilder.Entity("Identity_Domain.Entities.Base.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdentityId")
                        .HasColumnType("int");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Identity_Domain.Entities.Base.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("IdentityRole", b =>
                {
                    b.Property<int>("IdentitiesId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("IdentitiesId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("IdentityRole");
                });

            modelBuilder.Entity("ClaimIdentity", b =>
                {
                    b.HasOne("Identity_Domain.Entities.Base.Claim", null)
                        .WithMany()
                        .HasForeignKey("ClaimsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity_Domain.Entities.Base.Identity", null)
                        .WithMany()
                        .HasForeignKey("IdentitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClaimRole", b =>
                {
                    b.HasOne("Identity_Domain.Entities.Base.Claim", null)
                        .WithMany()
                        .HasForeignKey("ClaimsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity_Domain.Entities.Base.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Identity_Domain.Entities.Base.RefreshToken", b =>
                {
                    b.HasOne("Identity_Domain.Entities.Base.Identity", "Identity")
                        .WithOne("Token")
                        .HasForeignKey("Identity_Domain.Entities.Base.RefreshToken", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("IdentityRole", b =>
                {
                    b.HasOne("Identity_Domain.Entities.Base.Identity", null)
                        .WithMany()
                        .HasForeignKey("IdentitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity_Domain.Entities.Base.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Identity_Domain.Entities.Base.Identity", b =>
                {
                    b.Navigation("Token")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
