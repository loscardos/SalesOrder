using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Shared;

namespace SalesOrder.Server.Infrastructure;

public partial class OlDevContext : DbContext
{
    public OlDevContext()
    {
    }

    public OlDevContext(DbContextOptions<OlDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComCustomer> ComCustomers { get; set; }

    public virtual DbSet<SoItem> SoItems { get; set; }

    public virtual DbSet<SoOrder> SoOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComCustomer>(entity =>
        {
            entity.ToTable("COM_CUSTOMER");
            
            entity.Property(e => e.ComCustomerId).HasColumnName("COM_CUSTOMER_ID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_NAME");
        });

        modelBuilder.Entity<SoItem>(entity =>
        {
            entity.ToTable("SO_ITEM");

            entity.Property(e => e.SoItemId).HasColumnName("SO_ITEM_ID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("ITEM_NAME");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(-99)
                .HasColumnName("QUANTITY");
            entity.Property(e => e.SoOrderId)
                .HasDefaultValue(-99L)
                .HasColumnName("SO_ORDER_ID");
        });

        modelBuilder.Entity<SoOrder>(entity =>
        {
            entity.ToTable("SO_ORDER");

            entity.Property(e => e.SoOrderId).HasColumnName("SO_ORDER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.ComCustomerId)
                .HasDefaultValueSql("('-99')")
                .HasColumnName("COM_CUSTOMER_ID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("ORDER_NO");
        });
        
        modelBuilder.Entity<ComCustomer>()
            .HasMany(c => c.SoOrders)
            .WithOne(o => o.ComCustomer)
            .HasForeignKey(o => o.ComCustomerId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<SoOrder>()
            .HasMany(o => o.SoItems)
            .WithOne(i => i.SoOrder)
            .HasForeignKey(i => i.SoOrderId)
            .OnDelete(DeleteBehavior.Cascade); 

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
