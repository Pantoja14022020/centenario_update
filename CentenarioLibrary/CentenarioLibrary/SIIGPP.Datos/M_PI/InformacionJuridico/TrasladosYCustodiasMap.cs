using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.InformacionJuridico
{
    public class TrasladosYCustodiasMap : IEntityTypeConfiguration<TrasladosYCustodias>
    {
        public void Configure(EntityTypeBuilder<TrasladosYCustodias> builder)
        {
            builder.ToTable("PI_IJ_TRASLADOSYCUSTODIAS")
                    .HasKey(a => a.IdTrasladosYC);

            builder.Property(a => a.IdTrasladosYC)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdTrasladosYC)
           .HasDefaultValueSql("newId()");
        }
    }
}
