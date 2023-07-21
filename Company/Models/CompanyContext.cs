using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Company.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Empoyee> Empoyees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentDepartmentId).HasColumnName("ParentDepartmentID");

            entity.HasOne(d => d.ParentDepartment).WithMany(p => p.InverseParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .HasConstraintName("FK_Categories_ParentId");
        });

        modelBuilder.Entity<Empoyee>(entity =>
        {
            entity.ToTable("Empoyee");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DocNumber).HasMaxLength(6);
            entity.Property(e => e.DocSeries).HasMaxLength(4);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.SurName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Empoyees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empoyee_DepatmentId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
