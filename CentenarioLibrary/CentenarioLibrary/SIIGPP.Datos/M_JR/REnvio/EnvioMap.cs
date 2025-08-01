using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.REnvio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.REnvio
{
    public class EnvioMap : IEntityTypeConfiguration<Envio>
    {
  

        public void Configure(EntityTypeBuilder<Envio> builder)
        {
            builder.ToTable("JR_ENVIO")
                .HasKey(a => a.IdEnvio);
            builder.Property(a => a.FechaRegistro)
                .HasColumnType("DateTime");
            builder.Property(a => a.FechaCierre)
                .HasColumnType("DateTime");

            builder.Property(a => a.IdEnvio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ExpedienteId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdEnvio)
           .HasDefaultValueSql("newId()");
        }
    
    }
}
