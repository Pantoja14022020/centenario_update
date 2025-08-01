using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.Visitas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.Visitas
{
    public class VisitaMap : IEntityTypeConfiguration<Visita>
    {
        public void Configure(EntityTypeBuilder<Visita> builder)
        {
            builder.ToTable("PI_VISITA")
                    .HasKey(a => a.IdVisita);

            builder.Property(a => a.IdVisita)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PIPersonaVisitaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.DetencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdVisita)
           .HasDefaultValueSql("newId()");
        }
    }
}
