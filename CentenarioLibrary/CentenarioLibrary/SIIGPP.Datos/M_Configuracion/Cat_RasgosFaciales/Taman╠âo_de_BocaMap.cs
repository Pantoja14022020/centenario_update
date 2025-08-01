using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tamaño_de_BocaMap : IEntityTypeConfiguration<Tamaño_de_Boca>
    {
        public void Configure(EntityTypeBuilder<Tamaño_de_Boca> builder)
        {
            builder.ToTable("C_TAMAÑO_DE_BOCA")
                .HasKey(a => a.IdTamañoBoca);

            builder
           .Property(a => a.IdTamañoBoca)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTamañoBoca)
           .HasDefaultValueSql("newId()");
        }

    }
}
