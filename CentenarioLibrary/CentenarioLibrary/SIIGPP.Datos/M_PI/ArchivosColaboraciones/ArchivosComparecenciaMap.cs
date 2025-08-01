using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.ArchivosColaboraciones;
using System;
using System.Collections.Generic;
using System.Text;
namespace SIIGPP.Datos.M_PI.ArchivosColaboraciones
{
    public class ArchivosComparecenciaMap : IEntityTypeConfiguration<ArchivosComparecencia>
    {
        public void Configure(EntityTypeBuilder<ArchivosComparecencia> builder)
        {
            builder.ToTable("PI_ARCHIVOSCOMPARECENCIA")
                    .HasKey(a => a.IdrchivosComparecencia);

            builder.Property(a => a.IdrchivosComparecencia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PresentacionesYCId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdrchivosComparecencia)
           .HasDefaultValueSql("newId()");
        }
    }
}
