using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Generales
{
    public class RangoEdadMap : IEntityTypeConfiguration<RangoEdad>
    {
        public void Configure(EntityTypeBuilder<RangoEdad> builder)
        {
            builder.ToTable("C_RANGOEDAD")
                   .HasKey(a => a.IdRangoEdad);
            builder
            .Property(a => a.IdRangoEdad)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdRangoEdad)
           .HasDefaultValueSql("newId()");
        }
    }
}
