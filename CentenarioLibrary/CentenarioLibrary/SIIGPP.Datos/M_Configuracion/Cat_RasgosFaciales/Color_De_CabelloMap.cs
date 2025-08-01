using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Color_de_CabelloMap : IEntityTypeConfiguration<Color_de_Cabello>
    {
        public void Configure(EntityTypeBuilder<Color_de_Cabello> builder)
        {
            builder.ToTable("C_COLOR_DE_CABELLO")
                .HasKey(a => a.IdColorCabello);
            builder
           .Property(a => a.IdColorCabello)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdColorCabello)
           .HasDefaultValueSql("newId()");
        }

    }
}
