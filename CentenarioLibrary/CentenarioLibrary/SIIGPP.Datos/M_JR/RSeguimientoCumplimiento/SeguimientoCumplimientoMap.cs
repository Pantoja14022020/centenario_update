using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RSeguimientoCumplimiento
{
    public class SeguimientoCumplimientoMap : IEntityTypeConfiguration<SeguimientoCumplimiento>
    {
        public void Configure(EntityTypeBuilder<SeguimientoCumplimiento> builder)
        {
            builder.ToTable("JR_SEGUIMIENTOCUMPLIMIENTO")
            .HasKey(a => a.IdSeguimientoCumplimiento);
           

            builder.Property(a => a.IdSeguimientoCumplimiento)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.IdSeguimientoCumplimiento)
                   .HasDefaultValueSql("newId()");

            builder.Property(a => a.AcuerdoReparatorioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            

            builder.Property(a => a.Fecha)
               .HasColumnType("DateTime");

        }
    }
}
