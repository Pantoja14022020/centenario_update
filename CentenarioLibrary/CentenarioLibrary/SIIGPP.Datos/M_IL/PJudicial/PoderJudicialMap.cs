using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_IL.PJudicial;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_IL.PJudicial
{
    class PoderJudicialMap : IEntityTypeConfiguration<PoderJudicial>
    {
        public void Configure(EntityTypeBuilder<PoderJudicial> builder)
        {
            builder.ToTable("AUDIENCIAS").HasKey(a => a.IdSolicitud);
            builder.Property(a => a.IdSolicitud).HasColumnType("UNIQUEIDENTIFIER_ROWGUIDCOL").IsRequired();

        }
    }
}
