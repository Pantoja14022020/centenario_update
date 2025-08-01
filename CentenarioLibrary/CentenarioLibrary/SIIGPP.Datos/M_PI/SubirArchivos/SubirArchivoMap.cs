using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.SubirArchivos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.SubirArchivos
{
    public class SubirArchivoMap : IEntityTypeConfiguration<SubirArchivo>
    {
        public void Configure(EntityTypeBuilder<SubirArchivo> builder)
        {
            builder.ToTable("PI_SUBIRARCHIVO")
                    .HasKey(a => a.IdSubirArchivo);

            builder.Property(a => a.IdSubirArchivo)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DetencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdSubirArchivo)
           .HasDefaultValueSql("newId()");
        }
    }
}
