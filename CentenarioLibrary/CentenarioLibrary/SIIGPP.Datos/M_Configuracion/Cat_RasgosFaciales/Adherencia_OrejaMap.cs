using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Adherencia_OrejaMap : IEntityTypeConfiguration<Adherencia_Oreja>
    {
        public void Configure(EntityTypeBuilder<Adherencia_Oreja> builder)
        {
            builder.ToTable("C_ADHERENCIA_OREJA")
                .HasKey(a => a.IdAdherencia_Oreja);

            builder
           .Property(a => a.IdAdherencia_Oreja)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdAdherencia_Oreja)
           .HasDefaultValueSql("newId()");
        }

    }
}
