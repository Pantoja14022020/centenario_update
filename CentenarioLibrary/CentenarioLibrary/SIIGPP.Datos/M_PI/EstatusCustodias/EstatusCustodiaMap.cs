using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.EstatusCustodias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.EstatusCustodias
{
    public class EstatusCustodiaMap : IEntityTypeConfiguration<EstatusCustodia>
    {
        public void Configure(EntityTypeBuilder<EstatusCustodia> builder)
        {
            builder.ToTable("PI_ESTATUS_CUSTODIA")
                    .HasKey(a => a.IdEstatusCustodia);

            builder.Property(a => a.IdEstatusCustodia)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.DetencionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdEstatusCustodia)
           .HasDefaultValueSql("newId()");
        }
    }
}
