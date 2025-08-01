using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.ProcedimientoAbreviados;

namespace SIIGPP.Datos.M_Cat.ProcedimientoAbreviados
{
    public class ProcedimientoAbreviadosMap : IEntityTypeConfiguration<ProcedimientoAbreviado>
    {
        public void Configure(EntityTypeBuilder<ProcedimientoAbreviado> builder)
        {
            builder.ToTable("CAT_PROCEDIMIENTOABREVIADO")
                .HasKey(a => a.IdProcedimientoAbreviado);

            builder.Property(a => a.IdProcedimientoAbreviado)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdProcedimientoAbreviado)
           .HasDefaultValueSql("newId()");

        }
    }
}
