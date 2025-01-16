using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignmnet.EntityLayer.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Purchaseorder> Purchaseorders { get; set; }

    public virtual DbSet<Purchaseorderdetail> Purchaseorderdetails { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=assignment_database;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__material__3214EC07F528D8F3");

            entity.ToTable("material");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LongTexts).HasMaxLength(1000);
            entity.Property(e => e.ShortText).HasMaxLength(100);
        });

        modelBuilder.Entity<Purchaseorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__purchase__3214EC075D1DF37C");

            entity.ToTable("purchaseorder");

            entity.Property(e => e.OrderNo).HasMaxLength(50);
            entity.Property(e => e.OrderValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Purchaseorders)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK__purchaseo__Vendo__5070F446");
        });

        modelBuilder.Entity<Purchaseorderdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__purchase__3214EC07499DF37C");

            entity.ToTable("purchaseorderdetail");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Material).WithMany(p => p.Purchaseorderdetails)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__purchaseo__Mater__5535A963");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.Purchaseorderdetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .HasConstraintName("FK__purchaseo__Purch__5441852A");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vendor__3214EC075E63287B");

            entity.ToTable("vendor");

            entity.Property(e => e.AddressLine1).HasMaxLength(255);
            entity.Property(e => e.AddressLine2).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.ContactNo).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
