using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Datos.M_Configuracion.Cat_Vehiculos
{
    public class ColorMap : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("CV_COLOR")
                .HasKey(a => a.IdColor);
            builder
           .Property(a => a.IdColor)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdColor)
           .HasDefaultValueSql("newId()");
        }

    }
}
