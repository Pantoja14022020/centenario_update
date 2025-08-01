using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Datos.M_Configuracion.Cat_Armas
{
    public class CalibreMap : IEntityTypeConfiguration<Calibre>
    {
        public void Configure(EntityTypeBuilder<Calibre> builder)
        {
            builder.ToTable("CAR_CALIBRE")
                .HasKey(a => a.IdCalibre);
            builder
          .Property(a => a.IdCalibre)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdCalibre)
           .HasDefaultValueSql("newId()");
        }

    }
}
