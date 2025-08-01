using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RExpediente;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RExpediente
{
    public class ExpedienteMap : IEntityTypeConfiguration<Expediente>
    {
        public void Configure(EntityTypeBuilder<Expediente> builder)
        {
            builder.ToTable("JR_EXPEDIENTE")
            .HasKey(a => a.IdExpediente);

            builder.Property(a => a.FechaRegistroExpediente)
                .HasColumnType("DateTime");

            builder.Property(a => a.FechaDerivacion)
               .HasColumnType("DateTime");

            builder.Property(a => a.IdExpediente)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdExpediente)
           .HasDefaultValueSql("newId()");

        }
    }
}
