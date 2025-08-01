using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.ImpProceso;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.ImpProceso
{
    public class CondImpProcesoMap : IEntityTypeConfiguration<CondImpProceso>
    {
        public void Configure(EntityTypeBuilder<CondImpProceso> builder)
        {
            builder.ToTable("CAT_CONDIMPPRO")
                   .HasKey(a => a.idConImpProceso);
            builder.Property(a => a.FechaHoraCitacion)
                   .HasColumnType("DateTime");
            builder.Property(a => a.FechahoraComparecencia)
                  .HasColumnType("DateTime");
            builder.Property(a => a.FechaHoraEjecucionOrdenAprehecion)
                 .HasColumnType("DateTime");
            builder.Property(a => a.FechaHoraCancelacionOrdenAprehecion)
                 .HasColumnType("DateTime");
            builder.Property(a => a.FechaSys)
                 .HasColumnType("DateTime");

            builder.Property(a => a.idConImpProceso)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.idConImpProceso)
           .HasDefaultValueSql("newId()");

        }
    }
}
