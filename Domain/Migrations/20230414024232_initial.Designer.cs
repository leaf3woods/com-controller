﻿// <auto-generated />
using System;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(CtlDbContext))]
    [Migration("20230414024232_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Domain.Repos.Model.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Limsi")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("limsi");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("uri");

                    b.HasKey("Id");

                    b.HasIndex("Limsi")
                        .IsUnique();

                    b.HasIndex("Uri")
                        .IsUnique();

                    b.ToTable("device_info");
                });

            modelBuilder.Entity("Domain.Repos.Model.OperationLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("FlashInState")
                        .HasColumnType("INTEGER")
                        .HasColumnName("flash_in_state");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("operation_time");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("operation_log");
                });

            modelBuilder.Entity("Domain.Repos.Model.OperationLog", b =>
                {
                    b.HasOne("Domain.Repos.Model.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });
#pragma warning restore 612, 618
        }
    }
}
