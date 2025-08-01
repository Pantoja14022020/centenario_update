using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_SP.DocsDiligenciasForaneas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_SP.DocsDiligenciasForaneas
{
    public class DocsDiligenciaForaneasMap : IEntityTypeConfiguration<DocsDiligenciaForaneas>
    {
        public void Configure(EntityTypeBuilder<DocsDiligenciaForaneas> builder)
        {
            builder.ToTable("SP_DOCSDILIGENCIAFORANEAS")
                .HasKey(a => a.IdDocsDiligenciaForaneas);

            builder.Property(a => a.IdDocsDiligenciaForaneas)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RDiligenciasForaneasId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDocsDiligenciaForaneas)
           .HasDefaultValueSql("newId()");
        }

    }
}