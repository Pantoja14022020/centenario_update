using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;
namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class OrdenAprensionMap : IEntityTypeConfiguration<OrdenAprension>
    {
        public void Configure(EntityTypeBuilder<OrdenAprension> builder)
        {
            builder.ToTable("PI_IJ_ORDEN_APRENSION")
                    .HasKey(a => a.IdOrdenAprension);

            builder.Property(a => a.IdOrdenAprension)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdOrdenAprension)
           .HasDefaultValueSql("newId()");

        }
    }
}
