using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RAsignacionEnvios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RAsignacionEnvios
{
    class AsignacionEnvioMap : IEntityTypeConfiguration<AsignacionEnvio>
    {
        public void Configure(EntityTypeBuilder<AsignacionEnvio> builder)
        {

            builder.ToTable("JR_ASINGACIONENVIO")
                .HasKey(a => a.IdAsingacionEnvio);

            builder.Property(a => a.IdAsingacionEnvio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();


            builder.Property(a => a.ModuloServicioId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();

            builder.Property(a => a.EnvioId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();

            builder
            .Property(a => a.IdAsingacionEnvio)
            .HasDefaultValueSql("newId()");

        }
    }
}
