using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Color_OjosMap : IEntityTypeConfiguration<Color_Ojos>
    {
        public void Configure(EntityTypeBuilder<Color_Ojos> builder)
        {
            builder.ToTable("C_ COLOR_OJOS")
                .HasKey(a => a.IdColorOjos);

            builder
           .Property(a => a.IdColorOjos)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdColorOjos)
           .HasDefaultValueSql("newId()");



        }

    }
}
