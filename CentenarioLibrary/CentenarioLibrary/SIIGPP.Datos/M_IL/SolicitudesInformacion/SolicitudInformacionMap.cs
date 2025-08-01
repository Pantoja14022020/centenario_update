using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_IL.SolicitudesInformacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_IL.SolicitudesInformacion
{
    public class SolicitudInformacionMap : IEntityTypeConfiguration<SolicitudInformacion>
    {
        public void Configure(EntityTypeBuilder<SolicitudInformacion> builder)
        {
            builder.ToTable("IL_SOLICITUDES_DE_INFORMACION")
                    .HasKey(a => a.IdSolicitudInfo);

            builder.Property(a => a.IdSolicitudInfo)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdSolicitudInfo)
           .HasDefaultValueSql("newId()");
        }
    }
}
