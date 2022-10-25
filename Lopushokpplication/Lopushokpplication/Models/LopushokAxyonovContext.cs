using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lopushokpplication.Models
{
    public partial class LopushokAxyonovContext : DbContext
    {
        public LopushokAxyonovContext()
        {
        }

        public LopushokAxyonovContext(DbContextOptions<LopushokAxyonovContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; } = null!;
        public virtual DbSet<AgentPriorityHistory> AgentPriorityHistories { get; set; } = null!;
        public virtual DbSet<AgentType> AgentTypes { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialCountHistory> MaterialCountHistories { get; set; } = null!;
        public virtual DbSet<MaterialType> MaterialTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCostHistory> ProductCostHistories { get; set; } = null!;
        public virtual DbSet<ProductMaterial> ProductMaterials { get; set; } = null!;
        public virtual DbSet<ProductSale> ProductSales { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-I8L1GP6; Database=LopushokAxyonov; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agent_AgentType");
            });

            modelBuilder.Entity<AgentPriorityHistory>(entity =>
            {
                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentPriorityHistories)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentPriorityHistory_Agent");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasOne(d => d.MaterialType)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.MaterialTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_MaterialType");

                entity.HasMany(d => d.Suppliers)
                    .WithMany(p => p.Materials)
                    .UsingEntity<Dictionary<string, object>>(
                        "MaterialSupplier",
                        l => l.HasOne<Supplier>().WithMany().HasForeignKey("SupplierId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MaterialSupplier_Supplier"),
                        r => r.HasOne<Material>().WithMany().HasForeignKey("MaterialId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MaterialSupplier_Material"),
                        j =>
                        {
                            j.HasKey("MaterialId", "SupplierId");

                            j.ToTable("MaterialSupplier");

                            j.IndexerProperty<int>("MaterialId").HasColumnName("MaterialID");

                            j.IndexerProperty<int>("SupplierId").HasColumnName("SupplierID");
                        });
            });

            modelBuilder.Entity<MaterialCountHistory>(entity =>
            {
                entity.HasOne(d => d.Material)
                    .WithMany(p => p.MaterialCountHistories)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaterialCountHistory_Material");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_Product_ProductType");
            });

            modelBuilder.Entity<ProductCostHistory>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCostHistories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCostHistory_Product");
            });

            modelBuilder.Entity<ProductMaterial>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.MaterialId });

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.ProductMaterials)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductMaterial_Material");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductMaterials)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductMaterial_Product");
            });

            modelBuilder.Entity<ProductSale>(entity =>
            {
                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSale_Agent");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSale_Product");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shop_Agent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
