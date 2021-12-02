﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectAlpha.Data;

namespace ProjectAlpha.Migrations
{
    [DbContext(typeof(ProjectAlphaContext))]
    [Migration("20211202020154_image")]
    partial class image
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectAlpha.Models.Narsum", b =>
                {
                    b.Property<int>("NarsumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Keterangan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Narasumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NarsumID");

                    b.ToTable("Narsum");
                });

            modelBuilder.Entity("ProjectAlpha.Models.P2kp", b =>
                {
                    b.Property<int>("P2kpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("JamMulai")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("JamSelesai")
                        .HasColumnType("datetime2");

                    b.Property<string>("Judul")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NarsumID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tempat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WaktuBuat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WaktuSelesai")
                        .HasColumnType("datetime2");

                    b.HasKey("P2kpID");

                    b.HasIndex("NarsumID");

                    b.ToTable("P2kp");
                });

            modelBuilder.Entity("ProjectAlpha.Models.ViewModel.ImageP2kp", b =>
                {
                    b.Property<int>("ImageP2kpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("P2kpID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ImageP2kpID");

                    b.HasIndex("P2kpID");

                    b.ToTable("ImageP2kp");
                });

            modelBuilder.Entity("ProjectAlpha.Models.P2kp", b =>
                {
                    b.HasOne("ProjectAlpha.Models.Narsum", "Narsum")
                        .WithMany("P2Kps")
                        .HasForeignKey("NarsumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAlpha.Models.ViewModel.ImageP2kp", b =>
                {
                    b.HasOne("ProjectAlpha.Models.P2kp", "p2Kp")
                        .WithMany("ImageP2kp")
                        .HasForeignKey("P2kpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}