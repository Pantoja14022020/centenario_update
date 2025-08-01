using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_FEDC.NoAccionPenal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_FEDC.NoAccionPenal
{
    public class NoAcionPenalMap : IEntityTypeConfiguration<NoAcionPenal>
    {
        public void Configure(EntityTypeBuilder<NoAcionPenal> builder)
        {
            builder.ToTable("FEDC_NOACCIONPENAL")
                .HasKey(a => a.IdNoAcionPenal);

            builder.Property(a => a.IdNoAcionPenal)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdNoAcionPenal)
           .HasDefaultValueSql("newId()");

        }
    }
}
