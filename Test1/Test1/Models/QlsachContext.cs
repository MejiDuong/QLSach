using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Test1.Models;

public partial class QlsachContext : DbContext
{
    public QlsachContext()
    {
    }

    public QlsachContext(DbContextOptions<QlsachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TacGium> TacGia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MEJIDUONG-LAPTO\\DUONG;Initial Catalog=QLSach;Integrated Security=True;Trust Server Certificate=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach).HasName("PK__Sach__B235742D541F7605");

            entity.ToTable("Sach");

            entity.Property(e => e.MaSach)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaTg)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaTG");
            entity.Property(e => e.TenSach).HasMaxLength(50);

            entity.HasOne(d => d.MaTgNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaTg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sach_TacGia");
        });

        modelBuilder.Entity<TacGium>(entity =>
        {
            entity.HasKey(e => e.MaTg).HasName("PK__TacGia__2725007412A58B3E");

            entity.Property(e => e.MaTg)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaTG");
            entity.Property(e => e.TenTacGia).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
