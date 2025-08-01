using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.SolicitudesInteligencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_PI.SolicitudesInteligencia
{
    public class SolicitudInteligenciaMap : IEntityTypeConfiguration<SolicitudInteligencia>
    {
        public void Configure(EntityTypeBuilder<SolicitudInteligencia> builder)
        {
            builder.ToTable("PI_SOLICITUDINTELIGENCIA")
                .HasKey(a => a.IdSolicitudInteligencia);

            builder.Property(a => a.IdSolicitudInteligencia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PeritoAsignadoPIId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdSolicitudInteligencia)
           .HasDefaultValueSql("newId()");

        }

    }
}
