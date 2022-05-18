﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoItInCpp.Migrations
{
    [DbContext(typeof(DoItInCppContext))]
    [Migration("20220413001256_UMLUpdate1")]
    partial class UMLUpdate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DoItInCpp.Models.AddOn", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("AddOn", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.AddOnInSnippet", b =>
                {
                    b.Property<int>("SnippetID")
                        .HasColumnType("int");

                    b.Property<int>("AddOnID")
                        .HasColumnType("int");

                    b.HasKey("SnippetID", "AddOnID");

                    b.HasIndex("AddOnID");

                    b.ToTable("AddOnInSnippet", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.Include", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Include", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.IncludeEquivalence", b =>
                {
                    b.Property<int>("C_ID")
                        .HasColumnType("int");

                    b.Property<int>("CPP_ID")
                        .HasColumnType("int");

                    b.Property<int>("CPP_IncludeID")
                        .HasColumnType("int");

                    b.Property<int>("C_IncludeID")
                        .HasColumnType("int");

                    b.HasKey("C_ID", "CPP_ID");

                    b.HasIndex("CPP_IncludeID");

                    b.HasIndex("C_IncludeID");

                    b.ToTable("IncludeEquivalence", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.IncludeInSnippet", b =>
                {
                    b.Property<int>("SnippetID")
                        .HasColumnType("int");

                    b.Property<int>("IncludeID")
                        .HasColumnType("int");

                    b.HasKey("SnippetID", "IncludeID");

                    b.HasIndex("IncludeID");

                    b.ToTable("IncludeInSnippet", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.LanguageVersion", b =>
                {
                    b.Property<int>("Year_XX")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Year_XX"), 1L, 1);

                    b.HasKey("Year_XX");

                    b.ToTable("LanguageVersion", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.Snippet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Documentation")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VersionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("VersionID");

                    b.ToTable("Snippet", (string)null);
                });

            modelBuilder.Entity("DoItInCpp.Models.AddOnInSnippet", b =>
                {
                    b.HasOne("DoItInCpp.Models.AddOn", "AddOn")
                        .WithMany()
                        .HasForeignKey("AddOnID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoItInCpp.Models.Snippet", "Snippet")
                        .WithMany("AddOns")
                        .HasForeignKey("SnippetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddOn");

                    b.Navigation("Snippet");
                });

            modelBuilder.Entity("DoItInCpp.Models.IncludeEquivalence", b =>
                {
                    b.HasOne("DoItInCpp.Models.Include", "CPP_Include")
                        .WithMany()
                        .HasForeignKey("CPP_IncludeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoItInCpp.Models.Include", "C_Include")
                        .WithMany()
                        .HasForeignKey("C_IncludeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CPP_Include");

                    b.Navigation("C_Include");
                });

            modelBuilder.Entity("DoItInCpp.Models.IncludeInSnippet", b =>
                {
                    b.HasOne("DoItInCpp.Models.Include", "Include")
                        .WithMany()
                        .HasForeignKey("IncludeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoItInCpp.Models.Snippet", "Snippet")
                        .WithMany("Includes")
                        .HasForeignKey("SnippetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Include");

                    b.Navigation("Snippet");
                });

            modelBuilder.Entity("DoItInCpp.Models.Snippet", b =>
                {
                    b.HasOne("DoItInCpp.Models.LanguageVersion", "Version")
                        .WithMany()
                        .HasForeignKey("VersionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("DoItInCpp.Models.Snippet", b =>
                {
                    b.Navigation("AddOns");

                    b.Navigation("Includes");
                });
#pragma warning restore 612, 618
        }
    }
}
