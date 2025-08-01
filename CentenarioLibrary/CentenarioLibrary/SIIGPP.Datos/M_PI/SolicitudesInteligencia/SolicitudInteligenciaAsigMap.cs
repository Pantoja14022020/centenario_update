using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.SolicitudesInteligencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_PI.SolicitudesInteligencia
{
    public class SolicitudInteligenciaAsigMap : IEntityTypeConfiguration<SolicitudInteligenciaAsig>
    {
        public void Configure(EntityTypeBuilder<SolicitudInteligenciaAsig> builder)
        {
            builder.ToTable("PI_SOLICITUDINTELIGENCIAASIG")
                .HasKey(a => a.IdSolicitudInteligenciaAsig);

            builder.Property(a => a.IdSolicitudInteligenciaAsig)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.SolicitudInteligenciaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdSolicitudInteligenciaAsig)
           .HasDefaultValueSql("newId()");

        }

    }
}
