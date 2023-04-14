﻿// <auto-generated />
using System;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(CtlDbContext))]
    partial class CtlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("TEXT");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Limsi")
                        .IsUnique();

                    b.HasIndex("Uri")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Domain.Repos.Model.OperationLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("FlashInState")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("OperationLogs");
                });

            modelBuilder.Entity("Domain.Repos.Model.Device", b =>
                {
                    b.OwnsOne("Domain.Repos.Model.Setting", "Settings", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT");

                            b1.Property<string>("AccessPoint")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ApPassword")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ApUserId")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("MqttPassword")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("MqttPort")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("MqttServer")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("MqttUserName")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("Id");

                            b1.ToTable("Devices");

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.Navigation("Settings")
                        .IsRequired();
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
