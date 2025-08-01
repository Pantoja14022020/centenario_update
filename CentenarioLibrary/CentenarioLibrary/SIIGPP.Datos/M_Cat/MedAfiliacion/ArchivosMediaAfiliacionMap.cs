using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedAfiliacion
{
    public class ArchivosMediaAfiliacionMap : IEntityTypeConfiguration<ArchivosMediaAfiliacion>
    {
        public void Configure(EntityTypeBuilder<ArchivosMediaAfiliacion> builder)
        {
            builder.ToTable("PI_ARCHIVOSMEDIAAFILIACION")
                    .HasKey(a => a.IdArchivosMediaAfiliacion);

            builder.Property(a => a.IdArchivosMediaAfiliacion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.MediaAfiliacionid)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdArchivosMediaAfiliacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
