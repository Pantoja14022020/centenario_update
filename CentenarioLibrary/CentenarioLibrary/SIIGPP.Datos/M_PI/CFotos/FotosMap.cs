using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.CFotos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.CFotos
{
    public class FotosMap : IEntityTypeConfiguration<Fotos>
    {
        public void Configure(EntityTypeBuilder<Fotos> builder)
        {
            builder.ToTable("PI_FOTOS")
                    .HasKey(a => a.IdFotos);

            builder.Property(a => a.IdFotos)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RActoInvestigacionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdFotos)
           .HasDefaultValueSql("newId()");
        }
    }
}
