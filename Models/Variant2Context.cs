using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Variant2.Models;

public partial class Variant2Context : DbContext
{
    public Variant2Context()
    {
    }

    public Variant2Context(DbContextOptions<Variant2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=dbsrv\\dub2024;Database=Variant2;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC07F5A8CB32");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.Article, "UQ__Inventor__4943444A62D6A3E1").IsUnique();

            entity.Property(e => e.Article).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Inventory__UserI__3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07F7D3510E");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B635B01A7").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
