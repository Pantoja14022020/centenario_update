using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RSeguimientoCumplimiento
{
    public class AcuerdosConjuntoMap : IEntityTypeConfiguration<AcuerdosConjunto>
    {
        public void Configure(EntityTypeBuilder<AcuerdosConjunto> builder)
        {
            builder.ToTable("JR_ACUERDOS_CONJUNTOS")
            .HasKey(a => a.IdAC);
           

            builder.Property(a => a.IdAC)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdAC)
           .HasDefaultValueSql("newId()");

        }
    }
}
