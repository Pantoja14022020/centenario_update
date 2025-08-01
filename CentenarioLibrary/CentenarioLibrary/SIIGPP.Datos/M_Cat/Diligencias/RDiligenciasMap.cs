using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Diligencias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Diligencias
{
    public class RDiligenciasMap : IEntityTypeConfiguration<RDiligencias>
    {
        public void Configure(EntityTypeBuilder<RDiligencias> builder)
        {
            builder.ToTable("CAT_RDILIGENCIAS")
                     .HasKey(a => a.IdRDiligencias);

            builder.Property(a => a.IdRDiligencias)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.rHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.ASPId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRDiligencias)
           .HasDefaultValueSql("newId()");
        }
    }
}
