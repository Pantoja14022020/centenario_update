using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.PeritosAsignadosPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SIIGPP.Datos.M_PI.PeritosAsignadosPI
{
    public class PeritoAsignadoPIMap : IEntityTypeConfiguration<PeritoAsignadoPI>
    {
        public void Configure(EntityTypeBuilder<PeritoAsignadoPI> builder)
        {
            builder.ToTable("PI_PERITOSASIGNADOS")
                .HasKey(a => a.IdPeritoAsignadoPI);

            builder.Property(a => a.IdPeritoAsignadoPI)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RActoInvestigacionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdPeritoAsignadoPI)
           .HasDefaultValueSql("newId()");

        }

    }
}
