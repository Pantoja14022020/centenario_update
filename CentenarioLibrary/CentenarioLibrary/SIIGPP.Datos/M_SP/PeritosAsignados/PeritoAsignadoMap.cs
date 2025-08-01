using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_SP.PeritosAsignados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SIIGPP.Datos.M_SP.PeritosAsignado
{
    public class PeritoAsignadoMap : IEntityTypeConfiguration<PeritoAsignado>
    {
        public void Configure(EntityTypeBuilder<PeritoAsignado> builder)
        {
            builder.ToTable("SP_PERITOSASIGNADOS")
                .HasKey(a => a.IdPeritoAsignado);

            builder.Property(a => a.IdPeritoAsignado)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();


            builder.Property(a => a.RDiligenciasId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdPeritoAsignado)
           .HasDefaultValueSql("newId()");
        }

    }
}