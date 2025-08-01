using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.Detenciones;
using System;
using System.Collections.Generic;
using System.Text;
namespace SIIGPP.Datos.M_PI.Detenciones
{
    public class HistorialDetencionMap : IEntityTypeConfiguration<HistorialDetencion>
    {
        public void Configure(EntityTypeBuilder<HistorialDetencion> builder)
        {
            builder.ToTable("PI_HISTORIALDETENCIONES")
                    .HasKey(a => a.IdHistorialDetencion);

            builder.Property(a => a.IdHistorialDetencion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DetencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdHistorialDetencion)
           .HasDefaultValueSql("newId()");
        }
    }
}
