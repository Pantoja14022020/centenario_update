using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RNotificacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RNotificacion
{
    public class RegistroNotificacionesMap : IEntityTypeConfiguration<RegistroNotificacion>
    {
        public void Configure(EntityTypeBuilder<RegistroNotificacion> builder)
        {
            builder.ToTable("JR_REGISTRONOTIFICACION").HasKey(a => a.IdRegistroNotificaciones);
            builder.Property(a => a.FechaHora).HasColumnType("DateTime");

            builder.Property(a => a.IdRegistroNotificaciones)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ExpedienteId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRegistroNotificaciones)
           .HasDefaultValueSql("newId()");

        }
    }
}
