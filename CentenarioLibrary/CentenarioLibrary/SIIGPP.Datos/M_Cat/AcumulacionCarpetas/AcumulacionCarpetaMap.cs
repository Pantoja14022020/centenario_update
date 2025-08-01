using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.AcumulacionCarpetas;

namespace SIIGPP.Datos.M_Cat.AcumulacionCarpetas
{
    public class AcumulacionCarpetaMap : IEntityTypeConfiguration<AcumulacionCarpeta>
    {
        public void Configure(EntityTypeBuilder<AcumulacionCarpeta> builder)
        {
            builder.ToTable("CAT_ACUMULACION_CARPETA")
                    .HasKey(a => a.IdAcumulacionCarpeta);

            builder.Property(a => a.IdAcumulacionCarpeta)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdAcumulacionCarpeta)
           .HasDefaultValueSql("newId()");
        }
    }
}
