
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RAcuerdoReparatorio
{
    public class AcuerdoReparatorioMap : IEntityTypeConfiguration<AcuerdoReparatorio>
    {
        public void Configure(EntityTypeBuilder<AcuerdoReparatorio> builder)
        {
            builder.ToTable("JR_ACUERDOREPARATORIO")
                .HasKey(a => a.IdAcuerdoReparatorio);

            builder.Property(a => a.FechaCelebracionAcuerdo)
                .HasColumnType("DateTime");

            builder.Property(a => a.FechaLimiteCumplimiento)
                .HasColumnType("DateTime");

            builder.Property(a => a.FechaHoraRespuestaCoordinadorJuridico)
                .HasColumnType("DateTime");

            builder.Property(a => a.FechaRespuestaAMP)
            .HasColumnType("DateTime");

            builder.Property(a => a.Fechasise)
             .HasColumnType("DateTime");

            builder.Property(a => a.IdAcuerdoReparatorio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.EnvioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdAcuerdoReparatorio)
           .HasDefaultValueSql("newId()");


        }
    }
}
