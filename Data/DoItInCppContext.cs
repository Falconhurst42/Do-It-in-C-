#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

    public class DoItInCppContext : DbContext
    {
        public DoItInCppContext (DbContextOptions<DoItInCppContext> options)
            : base(options)
        {
        }

        public DbSet<DoItInCpp.Models.AddOn> AddOns { get; set; }
        public DbSet<DoItInCpp.Models.AddOnInSnippet> AddOnInSnippets { get; set; }
        public DbSet<DoItInCpp.Models.Include> Includes { get; set; }
        public DbSet<DoItInCpp.Models.IncludeEquivalence> IncludeEquivalences { get; set; }
        public DbSet<DoItInCpp.Models.IncludeInSnippet> IncludeInSnippets { get; set; }
        public DbSet<DoItInCpp.Models.Snippet> Snippets { get; set; }
        public DbSet<DoItInCpp.Models.LanguageVersion> LanguageVersions { get; set; }
        public DbSet<DoItInCpp.Models.Category> Categories { get; set; }
        public DbSet<DoItInCpp.Models.CategoryInSnippet> CategoryInSnippets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddOn>().ToTable("AddOn");
            modelBuilder.Entity<AddOnInSnippet>().ToTable("AddOnInSnippet");
            modelBuilder.Entity<Include>().ToTable("Include");
            modelBuilder.Entity<IncludeEquivalence>().ToTable("IncludeEquivalence");
            modelBuilder.Entity<IncludeInSnippet>().ToTable("IncludeInSnippet");
            modelBuilder.Entity<Snippet>().ToTable("Snippet");
            modelBuilder.Entity<LanguageVersion>().ToTable("LanguageVersion");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<CategoryInSnippet>().ToTable("CategoryInSnippet");

            modelBuilder.Entity<AddOnInSnippet>()
                .HasKey(c => new { c.SnippetID, c.AddOnID });
            modelBuilder.Entity<IncludeEquivalence>()
                .HasKey(c => new { c.C_ID, c.CPP_ID });
            modelBuilder.Entity<IncludeInSnippet>()
                .HasKey(c => new { c.SnippetID, c.IncludeID });
            modelBuilder.Entity<LanguageVersion>()
                .HasKey(c => new { c.Year_XX });
            modelBuilder.Entity<CategoryInSnippet>()
                .HasKey(c => new { c.SnippetID, c.CategoryID });

            // default values for dates https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
            modelBuilder.Entity<AddOn>()
                .Property(d => d.Created)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<AddOn>()
                .Property(d => d.LastUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Snippet>()
                .Property(d => d.Created)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Snippet>()
                .Property(d => d.LastUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");
        }
    }
