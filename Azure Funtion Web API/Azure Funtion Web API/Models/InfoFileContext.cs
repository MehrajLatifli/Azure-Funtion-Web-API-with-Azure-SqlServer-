﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Azure_Funtion_Web_API.Models
{
    public partial class InfoFileContext : DbContext
    {
        public InfoFileContext()
        {
        }

        public InfoFileContext(DbContextOptions<InfoFileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InfoFiles> InfoFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:mehracsql2.database.windows.net,1433;Initial Catalog=InfoFile;Persist Security Info=False;User ID=Mehrac1234;Password=Merac_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoFiles>(entity =>
            {
                entity.HasKey(e => e.IdFile)
                    .HasName("PK__InfoFile__01E644E1C507D4DE");

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.FilePath).IsRequired();

                entity.Property(e => e.FileSize).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}