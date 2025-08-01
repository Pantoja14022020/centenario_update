using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Datos.M_Configuracion.Cat_Armas
{
    public class ClasificacionArmaMap : IEntityTypeConfiguration<ClasificacionArma>
    {
        public void Configure(EntityTypeBuilder<ClasificacionArma> builder)
        {
            builder.ToTable("CAR_CLASIFICACIONARMAOB")
                .HasKey(a => a.IdClasificacionArma);

            builder
          .Property(a => a.IdClasificacionArma)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdClasificacionArma)
           .HasDefaultValueSql("newId()");
        }

    }
}
