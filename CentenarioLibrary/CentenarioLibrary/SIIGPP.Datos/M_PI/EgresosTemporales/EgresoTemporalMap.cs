using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.EgresosTemporales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.EgresosTemporales
{
    public class EgresoTemporalMap : IEntityTypeConfiguration<EgresoTemporal>
    {
        public void Configure(EntityTypeBuilder<EgresoTemporal> builder)
        {
            builder.ToTable("PI_EGRESO_TEMPORALES")
                    .HasKey(a => a.IdEgresoTemporal);

            builder.Property(a => a.IdEgresoTemporal)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DetencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdEgresoTemporal)
           .HasDefaultValueSql("newId()");
        }
    }
}
