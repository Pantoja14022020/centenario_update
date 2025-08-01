using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.ArchivosColaboraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.ArchivosColaboraciones
{
    public class ArchivosOAprensionMap : IEntityTypeConfiguration<ArchivosOAprension>
    {
        public void Configure(EntityTypeBuilder<ArchivosOAprension> builder)
        {
            builder.ToTable("PI_ARCHIVOSOAPRENSION")
                    .HasKey(a => a.IdArchivosOAprension);

            builder.Property(a => a.IdArchivosOAprension)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.OrdenAprensionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdArchivosOAprension)
           .HasDefaultValueSql("newId()");
        }

    }
}
