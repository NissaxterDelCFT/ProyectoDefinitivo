using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDefinitivo.Models;

public partial class SystemcftContext : DbContext
{
    public SystemcftContext()
    {
    }

    public SystemcftContext(DbContextOptions<SystemcftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AsignaturaEstudiante> AsignaturaEstudiantes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<AsignaturaEstudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura_estudiantes");

            entity.HasIndex(e => e.Asignaturaid, "fk_Asignatura_has_Estudiantes_Asignatura_idx");

            entity.HasIndex(e => e.Estudiantesid, "fk_Asignatura_has_Estudiantes_Estudiantes1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Asignaturaid).HasColumnType("int(11)");
            entity.Property(e => e.Estudiantesid).HasColumnType("int(11)");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.AsignaturaEstudiantes)
                .HasForeignKey(d => d.Asignaturaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Asignatura_has_Estudiantes_Asignatura");

            entity.HasOne(d => d.Estudiantes).WithMany(p => p.AsignaturaEstudiantes)
                .HasForeignKey(d => d.Estudiantesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Asignatura_has_Estudiantes_Estudiantes1");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiantes");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido).HasMaxLength(45);
            entity.Property(e => e.Direccion).HasMaxLength(45);
            entity.Property(e => e.Edad).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Rut).HasMaxLength(45);
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nota");

            entity.HasIndex(e => e.Asignaturaid, "fk_nota_Asignatura1_idx");

            entity.HasIndex(e => e.Estudiantesid, "fk_table1_Estudiantes1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Asignaturaid).HasColumnType("int(11)");
            entity.Property(e => e.Estudiantesid).HasColumnType("int(11)");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Nota)
                .HasForeignKey(d => d.Asignaturaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nota_Asignatura1");

            entity.HasOne(d => d.Estudiantes).WithMany(p => p.Nota)
                .HasForeignKey(d => d.Estudiantesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_table1_Estudiantes1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
