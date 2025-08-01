using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.CMedicosPSR;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIIGPP.Datos.M_PI.CMedicosPSR
{
    public class CmedicosPSRMap : IEntityTypeConfiguration<CMedicoPSR>
    {
        public void Configure(EntityTypeBuilder<CMedicoPSR> builder)
        {
            builder.ToTable("PI_CMEDICOPSR")
                    .HasKey(a => a.IdCMedicoPSR);

            builder.Property(a => a.IdCMedicoPSR)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdCMedicoPSR)
           .HasDefaultValueSql("newId()");
        }
    }
}
