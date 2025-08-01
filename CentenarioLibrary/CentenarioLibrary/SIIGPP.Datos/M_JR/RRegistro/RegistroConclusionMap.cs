using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RRegistro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RRegistro
{
    public class RegistroConclusionMap : IEntityTypeConfiguration<RegistroConclusion>
    {
        public void Configure(EntityTypeBuilder<RegistroConclusion> builder)
        {
            builder.ToTable("JR_REGISTROCONCLUSION")
                   .HasKey(a => a.IdRegistroConclusion);
            builder.Property(a => a.FechaHora)
                   .HasColumnType("DateTime");

            builder.Property(a => a.IdRegistroConclusion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.EnvioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();


            builder
           .Property(a => a.IdRegistroConclusion)
           .HasDefaultValueSql("newId()");
        }
    }
}
