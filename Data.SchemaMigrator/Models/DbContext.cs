﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.SchemaMigrator.Models
{
    public partial class DbContext : DbContext
    {
        public DbContext()
        {
        }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Campaign1> Campaign1 { get; set; }
        public virtual DbSet<CampaignImageAssoc> CampaignImageAssoc { get; set; }
        public virtual DbSet<CampaignStaff> CampaignStaff { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<River> River { get; set; }
        public virtual DbSet<River1> River1 { get; set; }
        public virtual DbSet<Trash> Trash { get; set; }
        public virtual DbSet<TrashType> TrashType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<User1> User1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:dev-trashroulette.database.windows.net,1433;Initial Catalog=dev-trashroulette;Persist Security Info=False;User ID=SurfriderAdmin;Password=PlastiqueEnFolie!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign", "bi");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Campaign1>(entity =>
            {
                entity.ToTable("Campaign");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsAidriven).HasColumnName("IsAIDriven");

                entity.Property(e => e.Locomotion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.RiverId).HasColumnType("numeric(8, 0)");

                entity.HasOne(d => d.River)
                    .WithMany(p => p.Campaign1)
                    .HasForeignKey(d => d.RiverId)
                    .HasConstraintName("FK__Campaign__RiverI__282DF8C2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Campaign1)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Campaign__UserId__2B0A656D");
            });

            modelBuilder.Entity<CampaignImageAssoc>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.ImageId })
                    .HasName("PK__Campaign__E80FE5E951C5BC76");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignImageAssoc)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CampaignI__Campa__6EF57B66");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.CampaignImageAssoc)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CampaignI__Image__6FE99F9F");
            });

            modelBuilder.Entity<CampaignStaff>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.UserId })
                    .HasName("PK__Campaign__EE26065D3CBEF659");

                entity.ToTable("Campaign_Staff");

                entity.Property(e => e.HasBeenTrained)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsStaff)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignStaff)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Campaign___Campa__3E1D39E1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CampaignStaff)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Campaign___UserI__3F115E1A");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BlobName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ContainerUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.ToTable("Logs", "bi");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<River>(entity =>
            {
                entity.ToTable("River", "bi");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MeanDensityOfLitter).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<River1>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__River__C1F8DC59ACD56E3A");

                entity.ToTable("River");

                entity.Property(e => e.Cid)
                    .HasColumnName("CID")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.Candidat)
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.CodeEntite)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(86)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trash>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AiVersion)
                    .HasColumnName("AI_Version")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Aiversion1)
                    .HasColumnName("AIVersion")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BrandType)
                    .HasColumnName("Brand_Type")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Trash)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trash__CampaignI__619B8048");

                entity.HasOne(d => d.RelatedImage)
                    .WithMany(p => p.Trash)
                    .HasForeignKey(d => d.RelatedImageId)
                    .HasConstraintName("FK__Trash__RelatedIm__32AB8735");

                entity.HasOne(d => d.TrashType)
                    .WithMany(p => p.Trash)
                    .HasForeignKey(d => d.TrashTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trash__TrashType__160F4887");
            });

            modelBuilder.Entity<TrashType>(entity =>
            {
                entity.ToTable("Trash_Type");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "bi");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<User1>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmailConfirmed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Experience)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
