using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Historialcarpetas;
using SIIGPP.Entidades.M_Cat.Desglose;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Desgloses
{
    public class DesglosesMap : IEntityTypeConfiguration<Desglose>
    {
        public void Configure(EntityTypeBuilder<Desglose> builder)
        {
            builder.ToTable("CAT_DESGLOSES")
                    .HasKey(a => a.IdDesgloses);

            builder.Property(a => a.IdDesgloses)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDesgloses)
           .HasDefaultValueSql("newId()");
        }
    }
}
