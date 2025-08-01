using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Resoluciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Resoluciones
{
    public class ResolucionMap : IEntityTypeConfiguration<Resolucion>
    {
        public void Configure(EntityTypeBuilder<Resolucion> builder)
        {
            builder.ToTable("CAT_RESOLUCION")
                    .HasKey(a => a.IdResolucion);

            builder.Property(a => a.IdResolucion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdResolucion)
           .HasDefaultValueSql("newId()");
        }
    }
}
