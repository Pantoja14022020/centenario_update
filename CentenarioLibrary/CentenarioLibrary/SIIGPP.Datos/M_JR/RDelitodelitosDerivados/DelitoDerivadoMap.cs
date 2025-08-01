using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RDelitodelitosDerivados
{
    public class DelitoDerivadoMap : IEntityTypeConfiguration<DelitoDerivado>
    {
        public void Configure(EntityTypeBuilder<DelitoDerivado> builder)
        {
            builder.ToTable("JR_DELITODERIVADO")
                .HasKey(a => a.IdDelitoDerivado);

            builder.Property(a => a.IdDelitoDerivado)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.EnvioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();
            builder.Property(a => a.RDHId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdDelitoDerivado)
           .HasDefaultValueSql("newId()");
        }
    }
}
