using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Configuracion
{
    public class MedioNotificacionMap : IEntityTypeConfiguration<MedioNotificacion>
    {
        public void Configure(EntityTypeBuilder<MedioNotificacion> builder)
        {
            builder.ToTable("C_MEDIONOTIFICACION")
                  .HasKey(a => a.IdMedioNotificacion);
            builder
          .Property(a => a.IdMedioNotificacion)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdMedioNotificacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
