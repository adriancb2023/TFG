using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace TFG_V0._01.Modelo;

public partial class TfgContext : DbContext
{
    public TfgContext()
    {
    }

    public TfgContext(DbContextOptions<TfgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alerta> Alertas { get; set; }

    public virtual DbSet<Caso> Casos { get; set; }

    public virtual DbSet<Casosetiqueta> Casosetiquetas { get; set; }

    public virtual DbSet<Casosrelacionado> Casosrelacionados { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contacto> Contactos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Etiqueta> Etiquetas { get; set; }

    public virtual DbSet<Expediente> Expedientes { get; set; }

    public virtual DbSet<HistorialEstado> HistorialEstados { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Registrotiempo> Registrotiempos { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=tfg;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.4.5-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_spanish_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alerta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alertas");

            entity.HasIndex(e => e.IdCaso, "FK_alertas_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasDefaultValueSql("'0'")
                .HasColumnName("descripcion");
            entity.Property(e => e.EstadoAlerta)
                .HasMaxLength(50)
                .HasDefaultValueSql("'0'")
                .HasColumnName("estado_alerta");
            entity.Property(e => e.FechaAlerta).HasColumnName("fecha_alerta");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.Titulo)
                .HasMaxLength(250)
                .HasDefaultValueSql("'0'")
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_alertas_casos");
        });

        modelBuilder.Entity<Caso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("casos");

            entity.HasIndex(e => e.IdCliente, "FK_casos_clientes");

            entity.HasIndex(e => e.IdEstado, "FK_casos_estado");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .HasDefaultValueSql("''")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("id_cliente");
            entity.Property(e => e.IdEstado)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Casos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_casos_clientes");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Casos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_casos_estado");
        });

        modelBuilder.Entity<Casosetiqueta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("casosetiquetas");

            entity.HasIndex(e => e.IdCaso, "FK_casosetiquetas_casos");

            entity.HasIndex(e => e.IdEtiqueta, "FK_casosetiquetas_estado");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.IdEtiqueta)
                .HasColumnType("int(11)")
                .HasColumnName("id_etiqueta");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Casosetiqueta)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_casosetiquetas_casos");

            entity.HasOne(d => d.IdEtiquetaNavigation).WithMany(p => p.Casosetiqueta)
                .HasForeignKey(d => d.IdEtiqueta)
                .HasConstraintName("FK_casosetiquetas_estado");
        });

        modelBuilder.Entity<Casosrelacionado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("casosrelacionados");

            entity.HasIndex(e => e.IdCaso, "FK_casosrelacionados_casos");

            entity.HasIndex(e => e.IdCasoRelacionado, "FK_casosrelacionados_casos_2");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.IdCasoRelacionado)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso_relacionado");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.CasosrelacionadoIdCasoNavigations)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_casosrelacionados_casos");

            entity.HasOne(d => d.IdCasoRelacionadoNavigation).WithMany(p => p.CasosrelacionadoIdCasoRelacionadoNavigations)
                .HasForeignKey(d => d.IdCasoRelacionado)
                .HasConstraintName("FK_casosrelacionados_casos_2");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(100)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(100)
                .HasColumnName("apellido2");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .HasColumnName("direccion");
            entity.Property(e => e.Email1)
                .HasMaxLength(250)
                .HasColumnName("email1");
            entity.Property(e => e.Email2)
                .HasMaxLength(250)
                .HasColumnName("email2");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telf1)
                .HasMaxLength(250)
                .HasColumnName("telf1");
            entity.Property(e => e.Telf2)
                .HasMaxLength(250)
                .HasColumnName("telf2");
        });

        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contactos");

            entity.HasIndex(e => e.IdCaso, "FK_contactos_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'0'")
                .HasColumnName("email");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasDefaultValueSql("'0'")
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .HasDefaultValueSql("'0'")
                .HasColumnName("telefono");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'0'")
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Contactos)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_contactos_casos");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentos");

            entity.HasIndex(e => e.IdCaso, "FK_documentos_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaSubid)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnName("fecha_subid");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
            entity.Property(e => e.Ruta)
                .HasMaxLength(500)
                .HasDefaultValueSql("''")
                .HasColumnName("ruta");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_documentos_casos");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estado");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasDefaultValueSql("'0'")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("'0'")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Etiqueta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("etiquetas");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Expediente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("expedientes");

            entity.HasIndex(e => e.IdCaso, "FK_expedientes_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.Jurisdiccion)
                .HasMaxLength(250)
                .HasDefaultValueSql("'0'")
                .HasColumnName("jurisdiccion");
            entity.Property(e => e.Juzgado)
                .HasMaxLength(250)
                .HasDefaultValueSql("'0'")
                .HasColumnName("juzgado");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(100)
                .HasDefaultValueSql("'0'")
                .HasColumnName("num_expediente");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .HasDefaultValueSql("''")
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Expedientes)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_expedientes_casos");
        });

        modelBuilder.Entity<HistorialEstado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("historial_estados");

            entity.HasIndex(e => e.IdCaso, "FK_historial_estados_casos");

            entity.HasIndex(e => e.IdEstado, "FK_historial_estados_estado");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaCambio).HasColumnName("fecha_cambio");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.IdEstado)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'0'")
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.HistorialEstados)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_historial_estados_casos");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.HistorialEstados)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_historial_estados_estado");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notificaciones");

            entity.HasIndex(e => e.IdCaso, "FK_notificaciones_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaVencimiento)
                .HasDefaultValueSql("'0000-00-00'")
                .HasColumnName("fecha_vencimiento");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(250)
                .HasDefaultValueSql("'0'")
                .HasColumnName("mensaje");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_notificaciones_casos");
        });

        modelBuilder.Entity<Registrotiempo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("registrotiempos");

            entity.HasIndex(e => e.IdCaso, "FK_registrotiempos_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasDefaultValueSql("'0'")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");
            entity.Property(e => e.Horas)
                .HasPrecision(5, 2)
                .HasColumnName("horas");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Registrotiempos)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_registrotiempos_casos");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tareas");

            entity.HasIndex(e => e.IdCaso, "FK_tareas_casos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'0'")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValueSql("'0'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.IdCaso)
                .HasColumnType("int(11)")
                .HasColumnName("id_caso");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasDefaultValueSql("'0'")
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdCaso)
                .HasConstraintName("FK_tareas_casos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
