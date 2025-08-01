using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Datos.M_Configuracion.Cat_Armas
{
    public class MarcaArmaMap : IEntityTypeConfiguration<MarcaArma>
    {
        public void Configure(EntityTypeBuilder<MarcaArma> builder)
        {
            builder.ToTable("CAR_MARCA_ARMA")
                .HasKey(a => a.IdMarcaArma);
            builder
          .Property(a => a.IdMarcaArma)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdMarcaArma)
           .HasDefaultValueSql("newId()");
        }

    }
}
