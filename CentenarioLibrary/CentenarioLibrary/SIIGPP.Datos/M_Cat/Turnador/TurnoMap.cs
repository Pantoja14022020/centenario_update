using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Turnador;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Turnador
{
    public class TurnoMap : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            builder.ToTable("CAT_TURNO")
                   .HasKey(a => a.IdTurno);
            builder.Property(a => a.Serie)
                   .HasColumnType("nvarchar(1)");
            builder.Property(a => a.NoTurno)
                   .HasColumnType("int");
            builder.Property(a => a.FechaHoraInicio)
                   .HasColumnType("DateTime");
            builder.Property(a => a.FechaHoraFin)
                   .HasColumnType("DateTime");
            builder.Property(a => a.Status)
                   .HasColumnType("bit");

            builder.Property(a => a.IdTurno)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RAtencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.AgenciaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdTurno)
           .HasDefaultValueSql("newId()");
        }
    }
}
