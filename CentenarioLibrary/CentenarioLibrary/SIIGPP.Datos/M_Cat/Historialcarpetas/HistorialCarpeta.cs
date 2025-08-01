using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Historialcarpetas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Historialcarpetas
{
    public class HistorialcarpetasMap : IEntityTypeConfiguration<HistorialCarpeta>
    {
        public void Configure(EntityTypeBuilder<HistorialCarpeta> builder)
        {
            builder.ToTable("CAT_HISTORIAL_CARPETA")
                    .HasKey(a => a.IdHistorialcarpetas);

            builder.Property(a => a.IdHistorialcarpetas)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdHistorialcarpetas)
           .HasDefaultValueSql("newId()");
        }
    }
}
