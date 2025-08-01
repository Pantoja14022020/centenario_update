using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RSolicitanteRequerido
{
    public class SolicitanteRequerdioMap : IEntityTypeConfiguration<SolicitanteRequerido>
    {
        public void Configure(EntityTypeBuilder<SolicitanteRequerido> builder)
        {
            builder.ToTable("JR_SOLICITANTEREQUERIDO")
              .HasKey(a => a.IdRSolicitanteRequerido);

            builder.Property(a => a.IdRSolicitanteRequerido)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder.Property(a => a.EnvioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();
            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdRSolicitanteRequerido)
           .HasDefaultValueSql("newId()");
        }
    }
}
