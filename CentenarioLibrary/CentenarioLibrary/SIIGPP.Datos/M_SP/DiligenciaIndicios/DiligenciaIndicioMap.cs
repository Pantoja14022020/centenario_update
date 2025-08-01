using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_SP.DiligenciaIndicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SIIGPP.Datos.M_SP.DiligenciaIndicios
{
    public class DiligenciaIndicioMap : IEntityTypeConfiguration<DiligenciaIndicio>
    {
        public void Configure(EntityTypeBuilder<DiligenciaIndicio> builder)
        {
            builder.ToTable("SP_DILIGENCIAINDICIO")
                .HasKey(a => a.IdDiligenciaIndicio);

            builder.Property(a => a.IdDiligenciaIndicio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.IndiciosId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RDiligenciasId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdDiligenciaIndicio)
           .HasDefaultValueSql("newId()");


        }

    }
}
