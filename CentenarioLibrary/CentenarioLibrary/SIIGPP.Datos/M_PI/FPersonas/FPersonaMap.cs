using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.FPersonas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.FPersonas
{
    public class FPersonaMap : IEntityTypeConfiguration<FPersona>
    {
        public void Configure(EntityTypeBuilder<FPersona> builder)
        {
            builder.ToTable("PI_FPERSONAS")
                    .HasKey(a => a.IdFPersona);

            builder.Property(a => a.IdFPersona)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PIPersonaVisitaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdFPersona)
           .HasDefaultValueSql("newId()");
        }
    }
}
