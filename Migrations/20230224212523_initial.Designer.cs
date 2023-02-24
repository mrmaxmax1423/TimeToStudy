﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeToStudy.Models;

namespace TimeToStudy.Migrations
{
    [DbContext(typeof(EventContext))]
    [Migration("20230224212523_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeToStudy.Models.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ClassLength")
                        .HasColumnType("float");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassStartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CreditHours")
                        .HasColumnType("float");

                    b.HasKey("ClassId");

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            ClassId = 1,
                            ClassLength = 1.0,
                            ClassName = "Calculus II",
                            ClassStartTime = "11:00",
                            CreditHours = 3.0
                        },
                        new
                        {
                            ClassId = 2,
                            ClassLength = 2.0,
                            ClassName = "Chemistry",
                            ClassStartTime = "12:30",
                            CreditHours = 3.0
                        });
                });

            modelBuilder.Entity("TimeToStudy.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("EventLength")
                        .HasColumnType("float");

                    b.Property<DateTime?>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Reccuring")
                        .HasColumnType("bit");

                    b.Property<bool>("SetTime")
                        .HasColumnType("bit");

                    b.HasKey("EventId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            EventDescription = "1",
                            EventLabel = "2",
                            EventLength = 3.0,
                            EventTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2023),
                            Reccuring = false,
                            SetTime = false
                        },
                        new
                        {
                            EventId = 2,
                            EventDescription = "3",
                            EventLabel = "4",
                            EventLength = 5.0,
                            EventTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2023),
                            Reccuring = true,
                            SetTime = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}