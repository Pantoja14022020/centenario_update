using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.RActosInvestigacion
{
    public class ActosInDetalleMap : IEntityTypeConfiguration<ActosInDetalle>
    {
        public void Configure(EntityTypeBuilder<ActosInDetalle> builder)
        {
            builder.ToTable("CAT_RACTOSINDETALLES")
                    .HasKey(a => a.IdActosInDetetalle);

            builder.Property(a => a.IdActosInDetetalle)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RActosInvestigacionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RActosInvestigacionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdActosInDetetalle)
           .HasDefaultValueSql("newId()");
        }
    }
}
