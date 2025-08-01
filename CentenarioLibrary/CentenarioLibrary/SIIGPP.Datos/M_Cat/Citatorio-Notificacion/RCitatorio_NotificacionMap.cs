using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Citatorio_Notificacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Citatorio_Notificacion
{
    public class RCitatorio_NotificacionMap : IEntityTypeConfiguration<RCitatorio_Notificacion>
    {
        public void Configure(EntityTypeBuilder<RCitatorio_Notificacion> builder)
        {
            builder.ToTable("CAT_RCITATORIO_NOTIFICACION")
                 .HasKey(a => a.IdCitatorio_Notificacion);

            builder.Property(a => a.Puesto)
                  .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Usuario)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Subproc)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Agencia)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.Distrito)
                   .HasColumnType("nvarchar(500)");

            builder.Property(a => a.IdCitatorio_Notificacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdCitatorio_Notificacion)
           .HasDefaultValueSql("newId()");

        }

    }
}
