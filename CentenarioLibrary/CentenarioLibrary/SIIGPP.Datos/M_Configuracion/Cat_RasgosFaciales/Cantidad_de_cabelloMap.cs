using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Cantidad_de_cabelloMap : IEntityTypeConfiguration<Cantidad_de_cabello>
    {
        public void Configure(EntityTypeBuilder<Cantidad_de_cabello> builder)
        {
            builder.ToTable("C_CANTIDAD_DE_CABELLO")
                .HasKey(a => a.IdCantidadCabello);
            builder
           .Property(a => a.IdCantidadCabello)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdCantidadCabello)
           .HasDefaultValueSql("newId()");
        }

    }
}
