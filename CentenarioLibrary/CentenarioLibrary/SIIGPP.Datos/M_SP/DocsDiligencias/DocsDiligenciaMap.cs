using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_SP.DocsDiligencias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_SP.DocsDiligencias
{
    public class DocsDiligenciaMap : IEntityTypeConfiguration<DocsDiligencia>
    {
        public void Configure(EntityTypeBuilder<DocsDiligencia> builder)
        {
            builder.ToTable("SP_DOCSDILIGENCIA")
                .HasKey(a => a.IdDocsDiligencia);

            builder.Property(a => a.IdDocsDiligencia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RDiligenciasId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDocsDiligencia)
           .HasDefaultValueSql("newId()");
        }

    }
}