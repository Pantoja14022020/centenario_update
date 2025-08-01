using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_IL.Citatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_IL.Citatorios
{
    public class CitatorioMap : IEntityTypeConfiguration<Citatorio>
    {
        public void Configure(EntityTypeBuilder<Citatorio> builder)
        {
            builder.ToTable("IL_CITATORIO")
                    .HasKey(a => a.IdCitatorio);

            builder.Property(a => a.IdCitatorio)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdCitatorio)
           .HasDefaultValueSql("newId()");
        }
    }
}
