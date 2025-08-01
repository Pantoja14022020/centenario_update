using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RCitatorioRecordatorio
{
    public class CitatorioRecordatorioMap : IEntityTypeConfiguration<CitatorioRecordatorio>
    {
        public void Configure(EntityTypeBuilder<CitatorioRecordatorio> builder)
        {
            builder.ToTable("JR_CITATORIORECORDATORIO")
                .HasKey(a => a.IdCitatorioRecordatorio);
            builder.Property(a => a.FechaSys)
                .HasColumnType("DateTime");
            builder.Property(a => a.FechaHoraCita)
                .HasColumnType("DateTime");

            builder.Property(a => a.IdCitatorioRecordatorio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.SesionId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdCitatorioRecordatorio)
           .HasDefaultValueSql("newId()");


        }
    }
}
