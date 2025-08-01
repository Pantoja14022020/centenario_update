using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.OAprhensionBitacoras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIIGPP.Datos.M_PI.OAprhensionBitacoras
{
    public class OAprhensionBitacoraMap : IEntityTypeConfiguration<OAprhensionBitacora>
    {
        public void Configure(EntityTypeBuilder<OAprhensionBitacora> builder)
        {
            builder.ToTable("PI_OAPHENSIONBITACORA")
                .HasKey(a => a.IdOAprhensionBitacora);

            builder.Property(a => a.IdOAprhensionBitacora)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.OrdenAprensionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdOAprhensionBitacora)
           .HasDefaultValueSql("newId()");

        }

    }
}
